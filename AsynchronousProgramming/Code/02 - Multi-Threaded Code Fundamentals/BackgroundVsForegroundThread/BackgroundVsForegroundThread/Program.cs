using System;
using System.Threading;

namespace BackgroundVsForegroundThread
{
    public class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("Thread is starting, press ENTER to continue...");
                Console.ReadLine();
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
