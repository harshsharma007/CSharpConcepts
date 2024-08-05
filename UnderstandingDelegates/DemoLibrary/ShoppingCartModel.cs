using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoLibrary
{
    public class ShoppingCartModel
    {
        public delegate void MentionDiscount(decimal subTotal);
        public List<ProductModel> Items { get; set; } = new List<ProductModel>();

        public decimal GenerateTotal(MentionDiscount mentionSubtotal,
            /*
                Func has 16 overloads. In Func we can pass 16 different variables, if that's the case you are back to creating a delegate. It is one of the limitations
                for this simplicity model. We cannot name these things we just have to pass in the types. So, Func takes in 16 parameters and the last parameter is
                always the output. Func is a method that has a return value other than void. Func does not work with out parameters. If you have to do that you have to
                use a delegate.
            */
            Func<List<ProductModel>, decimal, decimal> calculateDiscountedTotal,
            /*
                Action does not returns a value, it returns a void.
            */
            Action<string> tellUserWeAreDiscounting)
        {
            /*
                The Sum method below is actually taking a Func of ProductModel and decimal. So, it is using a delegate called Func. And, it is one delegate where
                x is our ProductModel and we are passing in and saying take the price for each of them and add them together.
            */
            decimal subTotal = Items.Sum(x => x.Price);

            /*
                Something is not good with this method because we have a whole bunch of hard-coded items and that's not really wise. Because this library may be used
                by different applications in your company. If you have to recompile this library with new code that means you have to change all your applications by
                recompiling them and redeploying them and that's really not efficient.
                
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
            */

            mentionSubtotal(subTotal);

            tellUserWeAreDiscounting("We are applying your discount.");

            return calculateDiscountedTotal(Items, subTotal);
        }
    }
}
