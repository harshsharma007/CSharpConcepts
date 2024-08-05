using System;
using System.Threading;

namespace GiveThreadName
{
    public class Program
    {
        private const int REPETITIONS = 100;

        public static void DoWork()
        {
            for (int i = 0; i < REPETITIONS; i++)
            {
                Console.Write("B");
            }
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 9; i++)
            {
                Thread thread = new Thread(DoWork);
                thread.Name = "Thread " + i.ToString();
                thread.Start();
            }
        }
    }
}
