using System;

namespace PassByValueVsReference
{
    public class A
    {
        public int value;

        public A()
        {
            value = 0;
        }
    }

    internal class Program
    {
        static void Increment(int b)
        {
            b++;
        }

        static void Increment(A a)
        {
            a.value++;
        }

        static void Main(string[] args)
        {
            A a = new A();
            Increment(a);
            int b = a.value;
            Increment(b);

            Console.WriteLine(a.value);
            Console.WriteLine(b);
            Console.ReadLine();
        }
    }
}
