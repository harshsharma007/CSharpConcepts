using System;
using System.Threading;

namespace RaceCondition
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Start thread to display 5 stars
            Thread thread = new Thread(DoWork);
            thread.Start();

            // Display 5 additional stars
            DoWork();
        }

        // public static int i = 0;
        public static void DoWork()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write("*");
            }
        }
    }
}
