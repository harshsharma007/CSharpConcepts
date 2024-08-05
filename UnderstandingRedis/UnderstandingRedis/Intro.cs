using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnderstandingRedis
{
    public class Intro
    {
        /*
            Caching can make your website run faster, it can improve your desktop applications and it can reduce the stress in your database. Basically, if you figure
            out caching you can dramatically improve how well your application scales.
            
            Just to show the difference between accessing our cache and accessing the database, we have to Delay the response by 1.5 sec to meet the real world scenario.
            I'm not saying that your database wouldn't be as fast as a cache what I'm saying is I want to show a difference so you'll know it's accessing the database
            when it takes a second and a half.
            
            You'll know it's accessing the cache when it's instantaneous that doesn't mean that the database wouldn't be instantaneous in your testing. But when you
            get into a real-world scenario where maybe 10,000 people are hitting the database you'll see some slowness happen and the cache should hopefully help with
            that.
            
            To tackle that we are going to implement a caching layer now what this caching layer will do is these results if this were a real weather forecast these
            results would not change on a second by second basis. They could be the same results for a minute, 5 minutes, 10 minutes probably 15 minutes or more. It
            wouldn't be a big deal to have them only be update every 15 minutes.
            
            So we can do is we can take the results we get from the database the first time they're requested we can store those in a different database called a cache
            which is what Redis is going to do and then we can access those results from the cache for the next let's say 15 minutes.
            
            In our example, we'll do one minute and the reason one minute is because it's a great short period of time for a demo so you don't get impatient waiting for
            that cache to refresh and see a difference between live values and cached values. With that we are now hitting our cache server instead of our database
            server.
            
            They may ask what's the difference? There are two database servers?
            Yes an no, because on our SQL database, let's just pretend this is coming from SQL (this weather forecast), we have to do a query. We have to pull data from
            different spots. With a cache database we've already done the query we've got the results and so we do is we store the value in what's called a key value
            pair.
            
            So we say these are the weather results from this 15-minute interval. We store that in one spot usually in JSON data that's we're gonna do today, we're
            gonna store it in JSON data. So it's already in the object shape we need it for C# and they just pull it right back out with a Key Value pair lookup so it's
            very very quick and we can scale that cache very easily.
            
            So this takes a big load off of our database servers. How to implement this in C#?
            - First step is to go to the command prompt, we are going to use docker locally. Because we're going to set up a Redis server (install and configure it)
              on our machine. To install Redis below is the command:
              docker run --name my-redis -p 5002:6379 -d redis
            
            What is this command doing?
            It's going to run an image, which image? The Redis image and this is going to get the latest version of the Redis image. It's going to download it if I
            don't have it and then it's going to operate or run start that instance with a few settings:
            
            - One setting is the name of the Redis container.
            - Another is mapping the port over so we can access it. If I say localhost:5002 that would be my Redis server and that's internal map to 6379 on the
              internal container.
            - I could give a username password in order to be secure we're not going to do that we're going to keep this as simple as possible locally.
            - If you don't want to do a docker then we are also going to spool up a Redis instance in Azure (very cheap very easy and very quick) and when we do that
              we also connect to that.
            - You just need a connection string for Redis and it is ready to go.
            
            After running the command the instance of Redis will be ready and running. To get list of all containers below is the command:
            docker ps -a
            
            Command to execute something on a container interactively on my-redis container (sh stands for shell):
            docker exec -it my-redis sh
            
            After executing this command then we can execute the below command to enter into the command line interface for Redis:
            # redis-cli
            
            After executing the above command you will talk directly to the cache database. Redis is a database which is similar to Dictionary in C# because Redis
            contains Keys and Values. Keys contain unique values.
            
            To check if the Redis is up use the "Ping" command and it will return "PONG". Select first database in the list by using "Select 0" command. Now, we are
            inside the first database.
            
            To check the db size use below command:
            dbsize
            
            scan 0 will return the items in the list. It will return any Keys and Values you have. Initially it will return 0 Keys because we have nothing in our
            database yet.
            
            After this, come back to your application and add Nuget packages. We need to install the below packages to work with Redis:
            1. Microsoft.Extensions.Caching.StackExchangeRedis : This will allow us to use caching in our web application. It is using StackExchangeRedis version.
               You can use InMemory if you want to, I don't find it terribly useful except for demos. StackExchange is the most common form of Redis caching.
            
            2. StackExchange.Redis : This is the client to connect to the actual database. So both of these packages work hand in hand to connect us to Redis.
            
            In Startup.cs class we have to define the ConnectionString and InstanceName:
            
            InstanceName is the name of your application. It will prepend any tag as Redis is a Key Value pair and your Keys must be unique. So if you are shoving 
            stuff into the database left and right from multiple applications, the odds that you use the same key in more than one application go up.
                    
            Let's just say you got really sloppy in naming and you named the key based upon a person's name that's not a good enough key it's not unique necessarily. 
            Let's say you call it TimCorey. What if you have that same logic in a different application for a different purpose you'll overwrite the Key from one 
            application to the other that wouldn't be good.
            
            So instead what we're going to do is we will prepend to every key this value and let's call this "RedisDemo_". Now what this will do if you use that Key
            name "TimCorey" now it would be "RedisDemo_TimCorey".
            
            If your other application which happens to be BlazerDemo would be "BlazerDemo_TimCorey" and you wouldn't have a conflict. So that instance name just makes
            it sure that even if you use the same database cache for more than one application that you don't have a conflict.
            
            Internally you have to make sure that your keys are unique for your application but this means you don't have to worry about other application stepping on
            your toes unless they also use the same instance name which is why your instance name should be unique.
            
            To add connection string, edit appSettings.json file and add connection string after AllowedHosts with value. Then add a new folder with the name Extensions.
            And in this folder, add a new class "DistributedCacheExtensions". This class will make our life easier and it will contain a complex piece of code.
            
            By default, this class does not have a simple easy map like you'd see with dapper where you're just saying, give me a Model instead there's a string here
            that comes back to a full model. So we're going to have two methods in here a SetRecord() method and a GetRecord() method. So set saves the item into the
            cache and the get gets the item out of cache.
        */
    }
}
