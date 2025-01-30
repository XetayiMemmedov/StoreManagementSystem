namespace StoreManagment;

internal class Person : BaseModel
{
    public Person(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}