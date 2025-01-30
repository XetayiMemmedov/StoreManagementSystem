using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagment
{
    internal static class PrintHelper
    {
        internal static void PrintProducts(Product[] products)
        {
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"Id",-6}{"Name",-20}{"Price",-6}");
            Console.WriteLine(new string('-', 80));

            foreach (Product product in products)
            {
                if (product == null) continue;

                if (product.Name == "Undefined") continue;

                Console.WriteLine($"{product.Id,-6}{product.Name,-20}{product.Price,-6}");
            }

            Console.WriteLine(new string('-', 80));
        }

        internal static void PrintCustomers(Customer[] customers)
        {
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"Id",-6}{"Name",-20}{"Age",-6}");
            Console.WriteLine(new string('-', 80));

            foreach (Customer customer in customers)
            {
                if (customer == null) continue;

                Console.WriteLine($"{customer.Id,-6}{customer.Name,-20}{customer.Age,-6}");
            }

            Console.WriteLine(new string('-', 80));
        }

        internal static void PrintStores(Store[] stores)
        {
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"Id",-6}{"Name",-20}");
            Console.WriteLine(new string('-', 80));

            foreach (Store store in stores)
            {
                if (store.Name == "Undefined") continue;

                Console.WriteLine($"{store.Id,-6}{store.Name,-20}");
            }

            Console.WriteLine(new string('-', 80));
        }
        internal static void PrintSellers(Seller[] sellers)
        {
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"Id",-6}{"Name",-20}");
            Console.WriteLine(new string('-', 80));

            foreach (Seller seller in sellers)
            {
                if (seller.Name == "Undefined") continue;

                Console.WriteLine($"{seller.Id,-6}{seller.Name,-20}");
            }

            Console.WriteLine(new string('-', 80));
        }

        internal static void PrintReceipt(Receipt receipt)
        {
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"Seller:"}{receipt.Seller.Name}");
            Console.WriteLine(new string('-', 80));

            foreach (BuyItem item in receipt.BuyItems)
            {
                if (item == null) continue;

                Console.WriteLine($"{item.Product.Name,-20}{item.Product.Price,-6}{item.Count,-3}{item.Total,-5}");
            }

            Console.WriteLine(new string('-', 80));
        }
    }
}
