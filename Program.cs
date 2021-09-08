using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShoppingCart;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class MySerializableProductClass
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class Item {
        public string name;
        public int price;
    }

    class Program
    {
        //private static object jsonString;

        static void Main(string[] args)
        {
            string[] myArray_productByWeight_name = new string[10];
            int[] myArray_productByWeight_price = new int[10];

            string[] myArray_productByQuantity_name = new string[10];
            int[] myArray_productByQuantity_price = new int[10];

            using (StreamReader r = new StreamReader("/Users/adrianbalbuena/Projects/ShoppingCart/ShoppingCart/EmptyJSONFile.json"))
            {
                var json = r.ReadToEnd();
                var result = JsonConvert.DeserializeObject<List<MySerializableProductClass>>(json);

                int count = 0; 
                foreach (var item in result) {
                    if (count < 10)
                    {
                        myArray_productByWeight_name[count] = item.Name;
                        myArray_productByWeight_price[count] = item.Price;
                    }
                    else {
                        var count2 = count - 10; 
                        myArray_productByQuantity_name[count2] = item.Name;
                        myArray_productByQuantity_price[count2] = item.Price;
                    }
                    count++;
                    //Console.WriteLine(item.Name);
                }
            }

            //var productByWeight1 = new MySerializableProductClass { Name = "Coke", Price = 1 };
            //var productByWeight2 = new MySerializableProductClass { Name = "Bud Light", Price = 2 };
            //var productByWeight3 = new MySerializableProductClass { Name = "Cinnamon Toast Crunch", Price = 3 };
            //var productByWeight4 = new MySerializableProductClass { Name = "Cliff Bar", Price = 4 };
            //var productByWeight5 = new MySerializableProductClass { Name = "Milk", Price = 5 };
            //var productByWeight6 = new MySerializableProductClass { Name = "Bread", Price = 6 };
            //var productByWeight7 = new MySerializableProductClass { Name = "Coke", Price = 7 };
            //var productByWeight8 = new MySerializableProductClass { Name = "Eggo Waffles", Price = 8 };
            //var productByWeight9 = new MySerializableProductClass { Name = "Mac and Cheese", Price = 9 };
            //var productByWeight10 = new MySerializableProductClass { Name = "DiGiorno Pizza", Price = 10 };

            //var productByWeight1Str = JsonConvert.SerializeObject(productByWeight1);
            //var productByWeight2Str = JsonConvert.SerializeObject(productByWeight2);
            //var productByWeight3Str = JsonConvert.SerializeObject(productByWeight3);
            //var productByWeight4Str = JsonConvert.SerializeObject(productByWeight4);
            //var productByWeight5Str = JsonConvert.SerializeObject(productByWeight5);
            //var productByWeight6Str = JsonConvert.SerializeObject(productByWeight6);
            //var productByWeight7Str = JsonConvert.SerializeObject(productByWeight7);
            //var productByWeight8Str = JsonConvert.SerializeObject(productByWeight8);
            //var productByWeight9Str = JsonConvert.SerializeObject(productByWeight9);
            //var productByWeight10Str = JsonConvert.SerializeObject(productByWeight10);

            //foreach (string str in myArray_productByWeight)
            //{
            //    Console.WriteLine("here: " + str);
            //}

            Console.WriteLine("Welcome to the shopping cart!");
            var cart = new ShoppingCart();

            bool cont = true;
            while (cont)
            {
                Console.WriteLine("What would you like to do?");
                //Console.WriteLine("1. Add an item");
                Console.WriteLine("1. Select an item from the list of products to add");
                Console.WriteLine("2. Remove an item");
                Console.WriteLine("3. Print current cart");
                Console.WriteLine("4. Check out");
                Console.WriteLine("5. Print the receipt to a file");
                var menuSelection = Console.ReadLine();

                if (int.TryParse(menuSelection, out int selection))
                {
                    switch (selection)
                    {
                        case 1:
                            Console.WriteLine();
                            Console.WriteLine("1. Do you want a product by weight?");
                            Console.WriteLine("2. Do you want a product by quantity?");

                            var menuSelection2 = Console.ReadLine();
                            if (int.TryParse(menuSelection2, out int selection2))
                            {
                                switch (selection2)
                                {
                                    case 1:
                                        Console.WriteLine();

                                        for (int i = 0; i < 5; i++)
                                        {
                                            Console.WriteLine(i+1 + ") " + myArray_productByWeight_name[i]);
                                        }

                                        var type = true;
                                        while (type)
                                        {
                                            Console.WriteLine("\nWould you like to view the (a) PREVIOUS page (aka products 1 thru 5), (b) NEXT page (aka products 6 thru 10), (c) or are you ready to make a decision?");
                                             var nextPage = Console.ReadLine();

                                            if (nextPage == "a")
                                            {
                                                for (int i = 0; i < 5; i++)
                                                {
                                                    Console.WriteLine(i + 1 + ") " + myArray_productByWeight_name[i]);
                                                }
                                            }
                                            else if (nextPage == "b")
                                            {
                                                for (int i = 5; i < 10; i++)
                                                {
                                                    Console.WriteLine(i + 1 + ") " + myArray_productByWeight_name[i]);
                                                }
                                            }
                                            else { type = false; }
                                        }

                                        Console.WriteLine("\nSo which product # would you like to add?");

                                        var choice = Convert.ToInt32(Console.ReadLine());
                                        bool inRange = true;
                                        while (inRange)
                                        {
                                            if (choice > 10 || choice < 1)
                                            {
                                                Console.WriteLine("Sorry, that's not a valid choice!");
                                                Console.WriteLine("\nSo which product # would you like to add?");
                                                choice = Convert.ToInt32(Console.ReadLine());
                                            }
                                            else
                                            {
                                                inRange = false; 
                                            }
                                        }

                                        var name = myArray_productByWeight_name[choice-1];
                                        var pricePerOunce = myArray_productByWeight_price[choice-1];

                                        var weight = 0D;
                                        //var pricePerOunce = 0.0D;
                                        Console.WriteLine("\nHow much would you like to buy in ounces?");
                                        var weightStr = Console.ReadLine();
                                        if (double.TryParse(weightStr, out weight))
                                        {
                                            cart.AddProduct(new ProductByWeight(name, weight, pricePerOunce));
                                            Console.WriteLine();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Sorry, that's not a valid choice!");
                                        }
                                        break;

                                    case 2:
                                        Console.WriteLine();

                                        for (int i = 0; i < 5; i++)
                                        {
                                            Console.WriteLine(i + 1 + ") " + myArray_productByQuantity_name[i]);
                                        }

                                        type = true;
                                        while (type)
                                        {
                                            Console.WriteLine("\nWould you like to view the (a) PREVIOUS page (aka products 1 thru 5), (b) NEXT page (aka products 6 thru 10), (c) or are you ready to make a decision?");
                                            var nextPage = Console.ReadLine();

                                            if (nextPage == "a")
                                            {
                                                for (int i = 0; i < 5; i++)
                                                {
                                                    Console.WriteLine(i + 1 + ") " + myArray_productByQuantity_name[i]);
                                                }
                                            }
                                            else if (nextPage == "b")
                                            {
                                                for (int i = 5; i < 10; i++)
                                                {
                                                    Console.WriteLine(i + 1 + ") " + myArray_productByQuantity_name[i]);
                                                }
                                            }
                                            else { type = false; }
                                        }

                                        Console.WriteLine("\nSo which product # would you like to add?");

                                        choice = Convert.ToInt32(Console.ReadLine());

                                        inRange = true;
                                        while (inRange)
                                        {
                                            if (choice > 10 || choice < 1)
                                            {
                                                Console.WriteLine("Sorry, that's not a valid choice!");
                                                Console.WriteLine("\nSo which product # would you like to add?");
                                                choice = Convert.ToInt32(Console.ReadLine());
                                            }
                                            else
                                            {
                                                inRange = false;
                                            }
                                        }

                                        name = myArray_productByQuantity_name[choice-1];
                                        pricePerOunce = myArray_productByQuantity_price[choice-1];

                                        var quantity = 0;
                                        //var pricePerOunce = 0.0D;
                                        Console.WriteLine("\nHow much would you like to buy in ounces?");
                                        var quantityStr = Console.ReadLine();
                                        if (int.TryParse(quantityStr, out quantity))
                                        {
                                            cart.AddProduct(new ProductByQuantity(name, quantity, pricePerOunce));
                                            Console.WriteLine();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Sorry, that's not a valid choice!");
                                        }
                                        break;
                                       
                                    default:
                                        Console.WriteLine("Sorry, that's not a valid choice!");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Sorry, that's not a valid choice!");
                            }
                            //AddItem(ref cart);
                            break;
                        case 2:
                            Console.WriteLine();
                            RemoveItem(ref cart);
                            break;
                        case 3:
                            Console.WriteLine();
                            Console.WriteLine(cart.ToString());
                            break;
                        case 4:
                            Console.WriteLine();
                            CheckOut(cart);
                            cont = false;
                            break;
                        case 5:
                            Console.WriteLine();
                            string[] lines = { "Final Bill", "----------", cart.ToString() };
                            File.WriteAllLines("receipt_File.txt", lines);
                            cont = false; 
                            break;
                        default:
                            Console.WriteLine("Sorry, that's not a valid choice!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, that's not a valid choice!");
                }
            }

            //var weight1Again = JsonConvert.DeserializeObject<MySerializableProductClass>(productByWeight1Str);
            //var weight2Again = JsonConvert.DeserializeObject<MySerializableProductClass>(productByWeight2Str);
            //var weight3Again = JsonConvert.DeserializeObject<MySerializableProductClass>(productByWeight3Str);
            //var weight4Again = JsonConvert.DeserializeObject<MySerializableProductClass>(productByWeight4Str);
            //var weight5Again = JsonConvert.DeserializeObject<MySerializableProductClass>(productByWeight5Str);
            //var weight6Again = JsonConvert.DeserializeObject<MySerializableProductClass>(productByWeight6Str);
            //var weight7Again = JsonConvert.DeserializeObject<MySerializableProductClass>(productByWeight7Str);
            //var weight8Again = JsonConvert.DeserializeObject<MySerializableProductClass>(productByWeight8Str);
            //var weight9Again = JsonConvert.DeserializeObject<MySerializableProductClass>(productByWeight9Str);
            //var weight10Again = JsonConvert.DeserializeObject<MySerializableProductClass>(productByWeight10Str);

            Console.WriteLine("Thanks for using the shopping cart!");
            Console.ReadLine();
        }

        public static void AddItem(ref ShoppingCart cart)
        {
            Console.WriteLine("What is the name of the item?");
            var name = Console.ReadLine();

            Console.WriteLine("Is this item priced by quantity or weight?");
            Console.WriteLine("1. Quantity");
            Console.WriteLine("2. Weight");
            var type = Console.ReadLine();
            if (int.TryParse(type, out int typeSelection))
            {
                switch (typeSelection)
                {
                    case 1:
                        Console.WriteLine("How many would you like to buy?");
                        var quantity = 0;
                        var pricePerUnit = 0.0D;
                        var quantityStr = Console.ReadLine();
                        if (int.TryParse(quantityStr, out quantity))
                        {
                            Console.WriteLine("What is the price per unit?");
                            var priceStr = Console.ReadLine().Replace("$", "");
                            if (double.TryParse(priceStr, out pricePerUnit))
                            {
                                cart.AddProduct(new ProductByQuantity(name, quantity, pricePerUnit));
                            }
                            else
                            {
                                Console.WriteLine("Sorry, that's not a valid choice!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, that's not a vlaid choice!");
                        }
                        break;
                    case 2:
                        var weight = 0D;
                        var pricePerOunce = 0.0D;
                        Console.WriteLine("How much would you like to buy in ounces?");
                        var weightStr = Console.ReadLine();
                        if (double.TryParse(weightStr, out weight))
                        {
                            Console.WriteLine("What is the price per ounce?");
                            var pricePerOunceStr = Console.ReadLine().Replace("$", "");
                            if (double.TryParse(pricePerOunceStr, out pricePerOunce))
                            {
                                cart.AddProduct(new ProductByWeight(name, weight, pricePerOunce));
                            }
                            else
                            {
                                Console.WriteLine("Sorry, that's not a valid choice!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, that's not a vlaid choice!");
                        }
                        break;
                    default:
                        Console.WriteLine("Sorry, that's not a valid choice!");
                        break;
                }
            }
        }

        public static void RemoveItem(ref ShoppingCart cart)
        {
            Console.WriteLine("Here are the current contents of the cart:");
            Console.WriteLine(cart.ToString());
            Console.WriteLine("What item would you like to remove?");
            var indexStr = Console.ReadLine().Trim();

            if (int.TryParse(indexStr, out int index))
            {
                var prod = cart.GetProduct(index);
                if (prod is ProductByQuantity)
                {
                    Console.WriteLine("How many of that item would you like to remove?");
                }
                else
                {
                    Console.WriteLine("How much of that item would you like to remove?");
                }

                var amountStr = Console.ReadLine().Trim();
                if (double.TryParse(amountStr, out double amount))
                {
                    cart.RemoveProduct(prod.Code, amount);
                }

            }
            else
            {
                Console.WriteLine("Sorry, that's not a valid choice!");
            }
        }

        public static void CheckOut(ShoppingCart cart)
        {
            Console.WriteLine("Final Bill");
            Console.WriteLine("----------");
            Console.WriteLine(cart.ToString());
        }

        
    }
}
