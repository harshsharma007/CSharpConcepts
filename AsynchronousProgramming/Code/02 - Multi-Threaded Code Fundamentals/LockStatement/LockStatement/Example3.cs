using System;
using System.Threading;

namespace LockStatement
{
    internal class Example3
    {
        private static int value1 = 1;
        private static int value2 = 1;

        // Synchronization Object
        private static object syncObject = new object();

        // Thread work method
        public static void DoWork()
        {
            lock (syncObject)
            {
                if (value2 > 0)
                {
                    value2 = 0;
                }
            }
        }

        // Helper method to do the division
        public static void DoTheDivision()
        {
            lock (syncObject)
            {
                Console.WriteLine(value1 / value2);
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
