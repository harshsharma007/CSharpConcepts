using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnderstandingAsyncAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        /*
            Synchronous programming is work that gets done in sequence, Task A gets done then Task B then Task C and finally Task D. The problem is that no one else
            can do any work until Task D gets completed.
            
            Asynchronous programming allows us to fix that problem there are two big benefits here I see right away when you use it.
            - The first big benefit is in your user interface if Task B takes a long time that will lock up your user interface. By executing a Task asynchronously
            while their way, complete control can be given back to the user interface so it's still responsive.
            - The second benefit comes into play especially when Tasks C and D don't rely on Task B. In fact, may be none of the Tasks depending on each other. May
            they're all just doing things that need to be done we don't need to get data back in that case we can execute them in parallel that means as long as your 
            system has the resources it will try to do all four tasks at once. If each task took three seconds to execute instead of taking a total of 12 seconds to 
            complete it might only take 4 seconds.
            
            Recap:
            - async marks a method as an asynchronous method and the await is used to say wait on something. The reason we use the await because if we don't then we're
              gonna fire and forget it and go on to the next step which is what we had when we saw that the timer was written first. Because the timer continued on while
              this was downloading the websites. So we had to say nope wait for that but even though we're waiting for something it still gives control back to the
              user interface so that we can move it around we can see other things pop up other things could happen in fact we can even have other events kicked off
              or other tasks done by the user while they're waiting for this to complete.
            
            - So it really make much more use of your operating system much more use of your hardware of your computer because it can do a whole lot of work and yet
              still give the users the experience of a responsive application.
            
            - We also have an idea of parallel where we are running multiple tasks and then saying when all of them are done then go ahead and move on in this case
              await for all of these and then afterwards print out the results.
        */

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteSync_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            //var results = DemoMethod.RunDownloadSync();
            var results = DemoMethod.RunDownloadParallelSync();
            PrintResults(results);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: { elapsedMs }";
        }

        // Events need to have void return type, they cannot have Task as a return type. This is the one exception that C# knows about.
        private async void ExecuteAsync_Click(object sender, RoutedEventArgs e)
        {
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            /*
                ThrowIfCancellationRequested will throw an exception. So it's important to wrap our call in try/catch block.
            */
            try
            {
                var results = await DemoMethod.RunDownloadAsync(progress, cts.Token);
                PrintResults(results);
            }
            catch (OperationCanceledException)
            {
                resultsWindow.Text += $"The async downloaded was cancelled { Environment.NewLine }";
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: { elapsedMs }";
        }

        private void ReportProgress(object sender, ProgressReportModel e)
        {
            DashboardProgress.Value = e.PercentageComplete;
            PrintResults(e.SitesDownloaded);
        }

        private async void ExecuteParallelAsync_Click(object sender, RoutedEventArgs e)
        {
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var results = await DemoMethod.RunDownloadParallelAsyncV2(progress);
            PrintResults(results);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: { elapsedMs }";
        }

        private void CancelOperation_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }

        private void PrintResults(List<WebsiteDataModel> results)
        {
            resultsWindow.Text = "";
            foreach (var item in results)
            {
                resultsWindow.Text += $"{ item.WebsiteUrl } downloaded: { item.WebsiteData.Length } characters long. { Environment.NewLine }";
            }
        }
    }
}
