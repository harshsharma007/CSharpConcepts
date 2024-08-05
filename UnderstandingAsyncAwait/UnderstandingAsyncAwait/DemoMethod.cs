using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace UnderstandingAsyncAwait
{
    public static class DemoMethod
    {
        public static List<string> PrepData()
        {
            List<string> output = new List<string>();

            output.Add("https://www.yahoo.com");
            output.Add("https://www.google.com");
            output.Add("https://www.microsoft.com");
            output.Add("https://www.cnn.com");
            output.Add("https://www.amazon.com");
            output.Add("https://www.facebook.com");
            output.Add("https://www.twitter.com");
            output.Add("https://www.codeproject.com");
            output.Add("https://www.stackoverflow.com");
            output.Add("https://en.wikipedia.org/wiki/.NET_Framework");

            return output;
        }

        public static List<WebsiteDataModel> RunDownloadSync()
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();

            foreach (string site in websites)
            {
                WebsiteDataModel results = DownloadWebsite(site);
                output.Add(results);
            }

            return output;
        }

        public static List<WebsiteDataModel> RunDownloadParallelSync()
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();

            /*
                For Parallel.ForEach we are passing a List of string and then for each item we are going to do an Action. What site represents is each of the website.
                This is same essentially for RunDownloadSync except for the fact that ForEach it does each of these download in parallel to each other. Parallel.ForEach
                gets a list of things do each one in parallel the others.
                
                The difference between Parallel.ForEach and RunDownloadParallelAsync (where we created our own parallel execution). The difference is Parallel.ForEach
                do the task in Parallel in a synchronous way meaning it locks everything up until they're all done. So the longest download that's how long we have to
                wait for this to be done. It's a short amount of time then the code written in RunDownloadSync method, which actually downloads one at a time and the
                time is cumulative. There is benefit in using Parallel.ForEach but still it's synchronous.
            */

            Parallel.ForEach<string>(websites, (site) =>
            {
                WebsiteDataModel results = DownloadWebsite(site);
                output.Add(results);
            });

            return output;
        }

        public async static Task<List<WebsiteDataModel>> RunDownloadParallelAsyncV2(IProgress<ProgressReportModel> progress)
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            ProgressReportModel report = new ProgressReportModel();

            /*
                For Parallel.ForEach we are passing a List of string and then for each item we are going to do an Action. What site represents is each of the website.
                This is same essentially for RunDownloadSync except for the fact that ForEach it does each of these download in parallel to each other. Parallel.ForEach
                gets a list of things do each one in parallel the others.
                
                The difference between Parallel.ForEach and RunDownloadParallelAsync (where we created our own parallel execution). The difference is Parallel.ForEach
                do the task in Parallel in a synchronous way meaning it locks everything up until they're all done. So the longest download that's how long we have to
                wait for this to be done. It's a short amount of time then the code written in RunDownloadSync method, which actually downloads one at a time and the
                time is cumulative. There is benefit in using Parallel.ForEach but still it's synchronous.
                
                RunDownloadParallelAsyncV2 is actually better and faster than RunDownloadParallelAsync because it will return the smallest website name which is 
                downloaded first and returns the largest website which is downloaded last. Unlike in RunDownloadParallelAsync website is downloaded in an order they
                were added to the list.
            */

            await Task.Run(() =>
            {
                Parallel.ForEach<string>(websites, (site) =>
                {
                    WebsiteDataModel results = DownloadWebsite(site);
                    output.Add(results);

                    report.SitesDownloaded = output;
                    report.PercentageComplete = (output.Count * 100) / websites.Count;
                    progress.Report(report);
                });
            });

            return output;
        }

        private static WebsiteDataModel DownloadWebsite(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = client.DownloadString(websiteURL);

            return output;
        }

        /*
            async methods need to have Task as a return type. It is not recommended to use void return type for asyn methods. You can use the void return type in
            asynchronous event handlers, which require a void return type. For methods other than event handlers that don't return a value, you should return a Task
            instead, because an async method that returns void can't be awaited.
        */

        public static async Task<List<WebsiteDataModel>> RunDownloadAsync(IProgress<ProgressReportModel> progress, CancellationToken cancellationToken)
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            ProgressReportModel report = new ProgressReportModel();

            foreach (string site in websites)
            {
                /*
                    When you check the execution time of both Normal Execution & Async Execution they are nearly have the same execution time. The reason why the
                    execute on the same time or the same speed is because of the await keyword below.
                    
                    Here, what it's doing is calling a first website say download that but wait for it. So, it blocks this particular method from getting to the next
                    website and it blocks the execution for the next website. So let's solve this problem with the help of parallel execution.
                */

                WebsiteDataModel results = await Task.Run(() => DownloadWebsite(site));
                output.Add(results);

                /*
                    The logical place to add the CancellationToken is after we download our first website. The CancellationToken is activated if we say go ahead and
                    cancel that Task we're going to throw and exception essentially and then exception is going to be the operation cancelled exception.
                    
                    That allows us couple of benefits:
                    1. It will stop right away and go back to the caller but it's also going to allow us to do any kind of clean-up on the caller end that we need to
                       do before we continue. Say we have open connections or something like that we can go ahead and close them.
                    2. Another thing is we should do some cleanup in here if we need to before we actually throw this. IsCancellationRequested that can check to see
                       if the cancellation has been requested. We check the value of IsCancellationRequested in the if condition and do cleanup and then
                       ThrowIfCancellationRequested().
                */

                cancellationToken.ThrowIfCancellationRequested();

                report.SitesDownloaded = output;
                report.PercentageComplete = (output.Count * 100) / websites.Count;
                progress.Report(report);
            }
            return output;
        }

        public static async Task<List<WebsiteDataModel>> RunDownloadParallelAsync()
        {
            List<string> websites = PrepData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

            foreach (string site in websites)
            {
                /*
                    This is the bubble of work that is not done yet and I'm trying to do. List these little bubbles of work and they all are running at the same time.
                */
                tasks.Add(DownloadWebsiteAsync(site));
            }

            /*
                What WhenAll does is, it says I'm gonna pass in some tasks (a whole set of them) one or hundred and you just wait until all of them are done and when
                they all are done pass the result back to results variable.
                
                We can't achieve the functionality of Parallel.ForEach in WhenAll because of the fact that we don't monitor each of the task for their completion
                instead we wait for all of them to be done. So we don't have a real good mechanism to say okay now that you have done something go ahead and report back.
            */
            var results = await Task.WhenAll(tasks);

            return new List<WebsiteDataModel>(results);
        }

        private static async Task<WebsiteDataModel> DownloadWebsiteAsync(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = await client.DownloadStringTaskAsync(websiteURL);

            return output;
        }
    }
}
