using System;
using System.Threading;

namespace LockStatement
{
    internal class Example2
    {
        // Shared private fields
        private static int value1 = 1;
        private static int value2 = 1;

        #region Synchronization Object
        private static object syncObject = new object();
        #endregion

        // Thread work method
        public static void DoWork()
        {
            bool lockTaken = false;
            try
            {
                // Monitor.Enter(syncObject, ref lockTaken);
                Monitor.TryEnter(syncObject, TimeSpan.FromMilliseconds(50), ref lockTaken);

                if (value2 > 0)
                {
                    Console.WriteLine(value1 / value2);
                    value2 = 0;
                }
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(syncObject);
            }
        }

        // In Main method do something like below:
        public void MainMethod()
        {
            Thread t1 = new Thread(DoWork);
            Thread t2 = new Thread(DoWork);
            t1.Start();
            t2.Start();
        }
    }
}
