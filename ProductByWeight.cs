using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace ShoppingCart
{
    public class ProductByWeight : Product
    {
        private double ounces { get; set; }
        private double pricePerOunce { get; set; }

        public ProductByWeight(string name, double oz, double price)
        {
            ounces = oz;
            pricePerOunce = price;
            this.Name = name;
        }
        public override double Amount => ounces;
        public override double Price => ounces * pricePerOunce;
        public override void Reduce(double amount)
        {
            ounces -= amount;
        }
    }
}
