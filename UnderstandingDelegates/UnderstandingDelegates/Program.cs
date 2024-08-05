using System;
using System.Collections.Generic;
using DemoLibrary;

namespace UnderstandingDelegates
{
    /*
        A delegate as foundation all it is you pass in a method instead of a property or instead of a variable. So when you're passing in the information to a method,
        a delegate is the same kind of thing only you're passing a method.
    */

    class Program
    {
        /*
            Delegates are essentially methods you pass around and can execute in other locations and because it just matters about the signature, what it returns and
            what it passes in, we don't even have to have the coupling of knowing any kind of intermediate class structure where it's tying things together. So, this
            is really loosely coupled.
        */

        static ShoppingCartModel cart = new ShoppingCartModel();

        static void Main(string[] args)
        {
            PopulateCartWithDemoData();

            /*
                c2 indicates that I want this to be a money which in since I'm in the US is going to be a dollar sign and it's going to have two places after the
                decimal that's the two and so they'll show it formatted as money.
            */

            Console.WriteLine($"The total for the cart is {cart.GenerateTotal(SubTotalAlert, CalculateLeveledDiscount, AlertUser):C2}");
            Console.WriteLine();

            /*
                How to create GenerateTotal method inline?
                We can do so with the use of Anonymous method. Since it is anonymous we don't need to give it a name for our method because the delegate doesn't care
                all the delegate cares about is output type and input types.
            */
            decimal total = cart.GenerateTotal((subTotal) => Console.WriteLine($"The subtotal for cart 2 is {subTotal:C2}"),
                (products, subTotal) =>
                {
                    if (products.Count > 3)
                        return subTotal * 0.5M;
                    else
                        return subTotal;
                },
                (message) => Console.WriteLine($"Cart 2 Alert: { message }"));

            Console.WriteLine($"The total for Cart 2 is {total:C2}");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Please press any key to exit the application");
            Console.ReadKey();
        }

        private static void SubTotalAlert(decimal subTotal)
        {
            Console.WriteLine($"The subtotal is {subTotal:C2}");
        }

        private static void AlertUser(string message)
        {
            Console.WriteLine(message);
        }

        private static decimal CalculateLeveledDiscount(List<ProductModel> items, decimal subTotal)
        {
            if (subTotal > 100)
            {
                return subTotal * 0.80M;
            }
            else if (subTotal > 50)
            {
                return subTotal * 0.85M;
            }
            else if (subTotal > 10)
            {
                return subTotal * 0.95M;
            }
            else
            {
                return subTotal;
            }
        }

        private static void PopulateCartWithDemoData()
        {
            /*
                The "M" in the Price indicates that this is a decimal field so you have to use an M to signify that the actual numbers here are decimal and not double.
                Because by default they revert to double because it's much less space that they takes up and that's what use for most numbers in your decimal point
                but for money we really should use decimal because that's much more precise. It's precise to the 28th place or so after the decimal versus double is
                only precise to the 15th place, which is still very precise it's just not nearly as precise as what you need for your bank account.
            */

            cart.Items.Add(new ProductModel { ItemName = "Cereal", Price = 3.63M });
            cart.Items.Add(new ProductModel { ItemName = "Milk", Price = 2.95M });
            cart.Items.Add(new ProductModel { ItemName = "Strawberries", Price = 7.51M });
            cart.Items.Add(new ProductModel { ItemName = "Blueberries", Price = 8.84M });
        }
    }
}
