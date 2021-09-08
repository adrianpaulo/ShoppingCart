using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ShoppingCart
{
    public class Product
    {

        public string Name { get; set; }

        public Guid Code { get; private set; }
        public virtual double Amount { get; }
        public virtual double Price { get; }
        public virtual void Reduce(double amount)
        {
            throw new NotImplementedException();
        }
        public Product()
        {
            Code = new Guid();
        }

        public override string ToString()
        {
            return $"{Name} x{Amount} {Price:C}";
        }

    }
}
