namespace StoreManagment;

internal class Product : BaseModel
{
    private static int AutoIncremendId = 1;

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
        Id = AutoIncremendId++;
    }

    public string Name { get; set; }
    public decimal Price { get; set; }
}
