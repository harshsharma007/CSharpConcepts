using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnderstandingRedis.Extensions
{
    public static class DistributedCacheExtensions
    {
        /*
            recordId - is the key and it would be a unique identifier for the cached item.
            data - is whatever we are going to store in cache that's why it's generic because it could be anything. In our case it's going to be an array of Model.
            There are two different expirations for your cached items. These will allow you to specify what those values will be.
            
            ?? is a null coalescing operator. What it allows me to do is say use this value on the left but in the event that it's null use the next value in line.
            What the expression below means that if you don't give me a value for absoluteExpireTime, I am going to assume that you want to use the default of 60 seconds.
            Once you put this item in the cache it will live for a total of 1 minute, unless you give me a different time.
            
            To understand unusedExpireTime, let's say we have set absoluteExpireTime to 1 day and that's a long time to expire. We can set a sliding expiration and
            say yes if you're not accessing it for let's say 15 minutes or an hour go ahead and get a new data next time they ask. So the SlidingExpiration is based
            upon its use. So if a Key doesn't get used within this time span then it's going to expire even if the absoluteExpireTime has not been met.
            
            If the cache item is continously used or used enough that it doesn't trip the SlidingExpiration it will still expire at the AbsoluteExpirationRelativeToNow.
            If the SlidingExpiration is null then it is assumed not to be set meaning there is no SlidingExpiration means we only deal with 
            AbsoluteExpirationRelativeToNow.
        */

        public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusedExpireTime;

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        /*
            What happens if we pass in the recordId that doesn't exists?
            We have to apply a condition in if statement.
        */

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
