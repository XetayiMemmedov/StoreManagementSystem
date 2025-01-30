using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace StoreManagment
{
    internal class DataContext
    {
        public DataContext(Product[] products, Store[] stores, Admin[] admins, Customer[] customers, Seller[] sellers, Receipt[] receipts, BuyItem[] buyItems)
        {
            Products = products;
            Stores = stores;
            Admins = admins;
            Customers = customers;
            Sellers = sellers;
            Receipts = receipts;
            BuyItems = buyItems;
        }

        public DataContext()
        {
            Products = new Product[20];
            Products[0] = new Product("Saqqiz", 0.2m);
            Products[1] = new Product("Corek", 0.65m);
            Products[2] = new Product("Cofe", 1m);
            Products[3] = new Product("Et", 15m);

            Stores = new Store[2];
            Stores[0] = new Store("Araz", [], [Products[0], Products[1]]);
            Stores[1] = new Store("Rahat", [], [Products[2], Products[3]]);

            Admins = new Admin[1];
            Admins[0] = new Admin("Admin");

            Customers = new Customer[2];
            Customers[0] = new Customer("Kenan", 23);
            Customers[1] = new Customer("Eli", 34);

            Sellers = new Seller[2];
            Sellers[0] = new Seller("Ferid", Stores[0].Id);
            Sellers[1] = new Seller("Ehmed", Stores[1].Id);


            BuyItems = new BuyItem[3];
            BuyItems[0] = new BuyItem(Products[0], 3);
            BuyItems[1] = new BuyItem(Products[1], 1);
            BuyItems[2] = new BuyItem(Products[2], 2);

            Receipts = new Receipt[2];
            Receipts[0] = new Receipt(Sellers[0], [BuyItems[0], BuyItems[1]]);
            Receipts[0] = new Receipt(Sellers[1], [BuyItems[2]]);

            Users = new User[3];
            Users[0] = new User("admin", "1234", 1, Admins[0].Id, -1, -1);
            Users[1] = new User("satici", "1234", 2, -1, Sellers[0].Id, -1);
            Users[2] = new User("musteri", "1234", 3, -1, -1, Customers[0].Id);
            AppointSellerAuto();
        }

        private int _productIndex = 4;

        public User[] Users { get; set; }
        public Product[] Products { get; set; }
        public Store[] Stores { get; set; }
        public Admin[] Admins { get; set; }

        public Customer[] Customers { get; set; }
        public Seller[] Sellers { get; set; }
        public Receipt[] Receipts { get; set; }
        public BuyItem[] BuyItems { get; set; }

        public void AppointSellerAuto()
        {
            Stores[0].Sellers = new Seller[] { Sellers[0] };
            Stores[1].Sellers = new Seller[] { Sellers[1] };
        }
        public void AddProduct()
        {
            string name;
            string message = "Name:";

            do
            {
                Console.Write(message);
                name = Console.ReadLine();
                message = "Name already exits, try again:";
            } while (HasProduct(name));

            decimal price;
            message = "Price:";
            do
            {
                Console.Write(message);
                message = "Format is incorrect enter price again:";

            } while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0);

            var product = new Product(name, price);
            Products[_productIndex++] = product;
        }

        public bool HasProduct(string name)
        {
            foreach (var item in Products)
            {
                if (item == null) continue;

                if (item.Name == "Undefined") continue;

                if (name == item.Name) return true;
            }

            return false;
        }

        public void AddReceipt()
        {
            Console.WriteLine("Choose customer id:");
            PrintHelper.PrintCustomers(Customers);

            Console.Write("Enter customer id:");
            var customerId = int.Parse(Console.ReadLine());
            var customer = GetCustomer(customerId);

            if (customer.Name == "Undefined")
            {
                Console.WriteLine("Not found this customer");
                return;
            }

            Console.WriteLine($"Welcome {customer.Name}");

            Console.WriteLine("Choose store id:");
            PrintHelper.PrintStores(Stores);
            Console.Write("Enter store id:");
            var storeId = int.Parse(Console.ReadLine());
            var store = GetStore(storeId);

            if (store.Name == "Undefined")
            {
                Console.WriteLine("Not found this store");
                return;
            }
            Console.WriteLine($"{customer.Name} welcome the {store.Name}");

            Console.WriteLine("Choose products from list:");
            PrintHelper.PrintProducts(store.Products);
            Console.WriteLine("Enter product id and count[product id, product count]:");

            string[] productInputs = Console.ReadLine().Split(",");
            int productId = int.Parse(productInputs[0]);
            int productCount = int.Parse(productInputs[1]);

            var product = GetProduct(productId);

            var buyItem = new BuyItem(product, productCount);

            var receipt = new Receipt(GetSellerByStoreId(5), [buyItem]);
            PrintHelper.PrintReceipt(receipt);

        }

        public Customer GetCustomer(int id)
        {
            foreach (var item in Customers)
            {
                if (item == null) continue;

                if (item.Id == id) return item;
            }

            return new Customer("Undefined", 0);
        }

        public Store GetStore(int id)
        {
            foreach (var item in Stores)
            {
                if (item == null) continue;

                if (item.Id == id) return item;
            }

            return new Store("Undefined", []);
        }

        public Product GetProduct(int id)
        {
            foreach (var item in Products)
            {
                if (item == null) continue;

                if (item.Name == "Undefined") continue;

                if (item.Id == id) return item;
            }

            return new Product("Undefined", 0);
        }
        public int GetStoreIndex(int id, Store[] stores)
        {
            for (int i = 0; i < stores.Length; i++)
            {
                if (stores[i] == null) continue;

                if (stores[i].Name == "Undefined") continue;

                if (stores[i].Id == id) return i;
            }

            return -1;
        }
        public int GetSellerIndex(int id, Seller[] sellers)
        {
            for (int i = 0; i < sellers.Length; i++)
            {
                if (sellers[i] == null) continue;

                if (sellers[i].Name == "Undefined") continue;

                if (sellers[i].Id == id) return i;
            }

            return -1;
        }
        public int GetProductIndex(int id, Product[] products)
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i] == null) continue;

                if (products[i].Name == "Undefined") continue;

                if (products[i].Id == id) return i;
            }

            return -1;
        }

        public Seller GetSellerByStoreId(int storeId)
        {
            foreach (var item in Sellers)
            {
                if (item.StoreId == storeId)
                    return item;
            }

            return new Seller("Undefined", 0);
        }

        public Seller GetSeller(int sellerId)
        {
            foreach (var item in Sellers)
            {
                if (item.Id == sellerId)
                    return item;
            }

            return new Seller("Undefined", 0);
        }
        public Admin GetAdmin(int adminId)
        {
            foreach (var item in Admins)
            {
                if (item.Id == adminId)
                    return item;
            }

            return new Admin("Undefined");
        }

        public User GetUser(string username, string password)
        {
            foreach (var item in Users)
            {
                if (username == item.Username && password == item.Password)
                    return item;
            }

            return new User() { Username = "Undefined" };
        }
    }
}
