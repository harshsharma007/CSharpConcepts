using System;
using System.Threading;

namespace StartThread
{
    internal class Program
    {
        private const int REPETITIONS = 1000;

        public static void DoWork()
        {
            for (int i = 0; i < REPETITIONS; i++)
            {
                Console.Write("B");
            }
        }

        static void Main(string[] args)
        {
            // First way of starting a thread
            Thread thread = new Thread(new ThreadStart(DoWork));
            thread.Start();

            // Second way of starting a thread
            Thread threadOne = new Thread(DoWork);
            threadOne.Start();

            // Third way of starting a thread
            Thread threadTwo = new Thread(() => { DoWork(); });
            threadTwo.Start();

            for (int i = 0; i < REPETITIONS; i++)
            {
                Console.Write("A");
            }
        }
    }
}
