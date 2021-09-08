using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ShoppingCart
{
    public class ShoppingCart
    {
        private List<Product> contents = new List<Product>();

        public double Subtotal
        {
            get
            {
                return contents.Sum(x => x.Price);
            }
        }

        public double Taxes
        {
            get
            {
                return 0.07 * Subtotal;
            }
        }

        public double Total
        {
            get
            {
                return Subtotal + Taxes;
            }
        }

        public override string ToString()
        {
            var str = string.Empty;
            for (int i = 0; i < contents.Count; i++)
            {
                str += $"{i + 1}. {contents[i]}" + "\n";
            }
            str += $"Subtotal: {Subtotal:C}" + "\n";
            str += $"Taxes: {Taxes:C}" + "\n";
            str += $"Total: {Total:C}" + "\n";

            return str;
        }

        public void AddProduct(Product item)
        {
            contents.Add(item);
        }

        public Product GetProduct(int index)
        {
            return contents[index - 1];
        }

        public int RemoveProduct(int productIndex, double unitsToRemove)
        {
            RemoveProduct(contents[productIndex - 1].Code, unitsToRemove);
            return productIndex;
        }

        public Guid RemoveProduct(Guid productCode, double unitsToRemove)
        {
            Product productToRemove = null;
            //get the product by code, and REMEMBER TO LEAVE THE LOOP WHEN YOU FIND IT!!!
            foreach (var item in contents)
            {
                if (item.Code.Equals(productCode))
                {
                    productToRemove = item;
                    break;
                }
            }

            if (productToRemove == null)
            {
                //the product wasn't found, return an empty guid as the code.
                return Guid.Empty;
            }

            RemoveProduct(productToRemove, unitsToRemove);

            return productToRemove.Code;
        }

        private void RemoveProduct(Product p, double amount)
        {

            contents.Find(x => x.Code.Equals(p.Code)).Reduce(amount);
            //There are absolutely weird use cases you could get into with this implementation.
            //  all of those should be handled by a UI, which we don't have the luxary off at this junction.
            if (Math.Round(p.Amount, 2) == 0)
            {
                contents.Remove(p);
            }

        }


    }
}
