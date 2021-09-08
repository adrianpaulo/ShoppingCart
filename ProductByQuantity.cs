using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
namespace ShoppingCart
{
    public class ProductByQuantity : Product
    {
        private int quantity { get; set; }
        private double pricePerQuantity { get; set; }

        public ProductByQuantity(string name, int quantity, double price)
        {
            this.quantity = quantity;
            pricePerQuantity = price;
            this.Name = name;
        }
        public override double Amount => quantity;
        public override double Price { get => quantity * pricePerQuantity; }
        public override void Reduce(double amount)
        {
            //this enforces a standard removal philosophy -- removing a portion removes an entire unit
            quantity -= (int)Math.Ceiling(amount);
        }

    }
}
