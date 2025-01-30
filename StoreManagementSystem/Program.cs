using System.Data;
using static System.Formats.Asn1.AsnWriter;

namespace StoreManagment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dataContext = new DataContext();
            string username, password;
            User user;

            do
            {
                Console.Write("Username:");
                username = Console.ReadLine();
                Console.Write("Password:");
                password = Console.ReadLine();
                user = dataContext.GetUser(username, password);

            } while (user.Username == "Undefined");

            if (user.IsUser == 2)
            {
                var seller = dataContext.GetSeller(user.SellerId);
                Console.WriteLine($"Welcome {seller.Name}");

                var store = dataContext.GetStore(seller.StoreId);

                Console.WriteLine($"Store name:{store.Name}");
                PrintHelper.PrintProducts(store.Products);

                Console.WriteLine("Choose command,[add, remove]");

                string command = Console.ReadLine();

                if (command == "add")
                {
                    Console.Write("Enter the name:");
                    string name = Console.ReadLine();
                    Console.Write("Enter the price:");
                    decimal price = decimal.Parse(Console.ReadLine());

                    var product = new Product(name, price);
                    var products = new Product[store.Products.Length + 1];
                    products = [.. store.Products, product];
                    //for (int i = 0; i< store.Products.Length; i++)
                    //{
                    //    products[i] = store.Products[i];
                    //}
                    //products[products.Length - 1] = product;
                    store.Products = products;
                    PrintHelper.PrintProducts(store.Products);
                }
                else if (command == "remove")
                {
                    Console.WriteLine("Choose product id:");
                    int id = int.Parse(Console.ReadLine());
                    var index = dataContext.GetProductIndex(id, store.Products);
                    if (index == -1) return;
                    store.Products[index].Name = "Undefined";
                    PrintHelper.PrintProducts(store.Products);
                }
                else if (command == "update")
                {
                    //print product 
                    PrintHelper.PrintProducts(store.Products);
                    Console.Write("Enter product id:");
                    int id = int.Parse(Console.ReadLine());
                    var index = dataContext.GetProductIndex(id, store.Products);
                    if (index == -1) return;

                    Console.Write("Enter new price:");
                    decimal price = decimal.Parse(Console.ReadLine());
                    store.Products[index].Price = price;
                    PrintHelper.PrintProducts(store.Products);
                    //input product price
                    //product.price=price
                    //print product
                }

            }
            else if (user.IsUser == 2)
            {
                var customer = dataContext.GetCustomer(user.CustomerId);
                Console.WriteLine($"Welcome {customer.Name}");

                Console.WriteLine("Choose store id:");
                PrintHelper.PrintStores(dataContext.Stores);
                Console.Write("Enter store id:");
                var storeId = int.Parse(Console.ReadLine());
                var store = dataContext.GetStore(storeId);

                if (store.Name == "Undefined")
                {
                    Console.WriteLine("Not found this store");
                    return;
                }
                Console.WriteLine($"{customer.Name} welcome the {store.Name}");

                Console.WriteLine("Choose products from list:");
                PrintHelper.PrintProducts(store.Products);
                Console.WriteLine("Enter product id and count[product id, product count], for leave type exit:");

                string command;
                var buyItems = new BuyItem[10];
                int buyItemIndex = 0;
                do
                {
                    command = Console.ReadLine();

                    if (command == "exit") break;

                    string[] productInputs = command.Split(",");
                    int productId = int.Parse(productInputs[0]);
                    int productCount = int.Parse(productInputs[1]);

                    var product = dataContext.GetProduct(productId);

                    var buyItem = new BuyItem(product, productCount);
                    buyItems[buyItemIndex++] = buyItem;
                } while (true);

                decimal total = 0;
                foreach (var item in buyItems)
                {
                    if (item == null) continue;
                    total += item.Total;
                }

                if (customer.Balance < total)
                {
                    Console.WriteLine($"You dont have enough balance.You need {total - customer.Balance}");
                    return;
                }

                var receipt = new Receipt(dataContext.GetSellerByStoreId(storeId), buyItems);
                PrintHelper.PrintReceipt(receipt);
            }
            else if (user.IsUser == 1)
            {
                string command;
                var admin = dataContext.GetAdmin(user.AdminId);
                Console.WriteLine($"Welcomee {admin.Name}");
                do
                {
                    Console.WriteLine("Choose command: [add store, remove store, add seller, remove seller]");
                    command = Console.ReadLine();

                    if (command == "add store")
                    {
                        Console.Write("Enter the name:");
                        string name = Console.ReadLine();
                        var store = new Store(name, []);
                        var stores = new Store[dataContext.Stores.Length + 1];
                        stores = [.. dataContext.Stores, store];
                        stores[stores.Length - 1] = store;
                        dataContext.Stores = stores;
                        PrintHelper.PrintStores(stores);
                    }
                    else if (command == "remove store")
                    {
                        Console.WriteLine("Choose Store id:");
                        int id = int.Parse(Console.ReadLine());
                        var index = dataContext.GetStoreIndex(id, dataContext.Stores);
                        if (index == -1) return;
                        dataContext.Stores[index].Name = "Undefined";
                        PrintHelper.PrintStores(dataContext.Stores);
                    }

                    else if (command == "add seller")
                    {
                        Console.Write("Enter the name:");
                        string name = Console.ReadLine();
                        Console.Write("Enter the storid:");
                        int storeId = int.Parse(Console.ReadLine());
                        var seller = new Seller(name, storeId);
                        var sellers = new Seller[dataContext.Sellers.Length + 1];
                        sellers = [.. dataContext.Sellers, seller];
                        sellers[sellers.Length - 1] = seller;
                        dataContext.Sellers = sellers;
                        PrintHelper.PrintSellers(sellers);
                    }
                    else if (command == "remove seller")
                    {
                        Console.WriteLine("Choose Seller id:");
                        int id = int.Parse(Console.ReadLine());
                        var index = dataContext.GetSellerIndex(id, dataContext.Sellers);
                        if (index == -1) return;
                        dataContext.Sellers[index].Name = "Undefined";
                        PrintHelper.PrintSellers(dataContext.Sellers);
                    }

                }
                while (command != "exit");

            }
        }
    }
}
