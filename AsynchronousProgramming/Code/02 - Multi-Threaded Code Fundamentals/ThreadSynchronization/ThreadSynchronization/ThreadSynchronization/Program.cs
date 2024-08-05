using System;
using System.Threading;

namespace ThreadSynchronization
{
    internal class Program
    {
        // Shared field for work result
        public static int result = 0;

        // Lock handle for shared result
        private static object lockHandle = new object();

        #region Event Wait Handles
        public static EventWaitHandle readyForResult = new AutoResetEvent(false);
        public static EventWaitHandle setResult = new AutoResetEvent(false);
        #endregion

        static void Main(string[] args)
        {
            // Start the thread
            Thread thread = new Thread(DoWork);
            thread.Start();

            // Collect result every 10 MilliSeconds
            for (int i = 0; i < 100; i++)
            {
                // Tell thread that we're ready to receive the result
                readyForResult.Set();

                // Wait until thread has set the result
                setResult.WaitOne();

                lock (lockHandle)
                {
                    Console.WriteLine(result);
                }

                // Simulate other work
                Thread.Sleep(10);
            }

            // Messy abort
            thread.Abort();
        }

        public static void DoWork()
        {
            while (true)
            {
                int i = result;

                // Simulate long calculation
                Thread.Sleep(1);

                // Wait until main loop is ready to receive result
                readyForResult.WaitOne();

                // Return result
                lock (lockHandle)
                {
                    result = i + 1;
                }

                // Tell main loop that we set the result
                setResult.Set();
            }
        }
    }
}
