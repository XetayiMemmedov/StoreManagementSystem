namespace StoreManagment;

internal class Store : BaseModel
{
    private static int AutoIncremendId = 1;

    public Store(string name)
    {
        Name = name;
        //Sellers = sellers;
        Id = AutoIncremendId++;
    }

    public Store(string name, Product[] products)
    {
        Name = name;
        Products = products;
        Id = AutoIncremendId++;
    }

    public string Name { get; set; }
    // public Seller[] Sellers { get; set; }

    public Product[] Products { get; set; }
}