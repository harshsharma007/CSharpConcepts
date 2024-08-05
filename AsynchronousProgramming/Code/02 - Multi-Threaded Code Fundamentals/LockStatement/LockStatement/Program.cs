using System;
using System.Threading;

namespace LockStatement
{
    internal class Program
    {
        // Shared private fields
        private static int value1 = 1;
        private static int value2 = 1;

        #region Synchronization Object
        private static object syncObject = new object();
        #endregion

        static void Main(string[] args)
        {
            Thread threadOne = new Thread(DoWork);
            Thread threadTwo = new Thread(DoWork);
            threadOne.Start();
            threadTwo.Start();
        }

        public static void DoWork()
        {
            lock (syncObject)
            {
                if (value2 > 0)
                {
                    Console.WriteLine(value1 / value2);
                    value2 = 0;
                }
            }
        }
    }
}
