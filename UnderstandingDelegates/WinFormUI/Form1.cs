using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoLibrary;

namespace WinFormUI
{
    public partial class Form1 : Form
    {
        ShoppingCartModel cart = new ShoppingCartModel();

        public Form1()
        {
            InitializeComponent();
            PopulateCartWithDemoData();
        }

        private void PopulateCartWithDemoData()
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

        private void MessageBoxDemo_Click(object sender, EventArgs e)
        {
            decimal total = cart.GenerateTotal(SubTotalAlert, CalculateLeveledDiscount, PrintOutDiscountAlert);
            MessageBox.Show($"The total is {total:C2}");
        }

        private void TextBoxDemo_Click(object sender, EventArgs e)
        {
            decimal total = cart.GenerateTotal((subTotal) => SubTotalTextBox.Text = $"{subTotal:C2}",
                (products, subTotal) => subTotal - (products.Count * 2),
                (message) => { });

            TotalTextBox.Text = $"{total:C2}";
        }

        private void PrintOutDiscountAlert(string discountMessage)
        {
            MessageBox.Show(discountMessage);
        }

        private void SubTotalAlert(decimal subTotal)
        {
            MessageBox.Show($"The subtotal is {subTotal:C2}");
        }

        private decimal CalculateLeveledDiscount(List<ProductModel> products, decimal subTotal)
        {
            return subTotal - products.Count;
        }
    }
}
