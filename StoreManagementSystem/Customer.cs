using System;

namespace StoreManagment;

internal class Customer : Person
{
    private static int AutoIncremendId = 1;

    public Customer(string name, int age) : base(name)
    {
        Age = age;
        Balance = 100;
        Id = AutoIncremendId++;
    }

    public int Age { get; set; }
    public decimal Balance { get; set; }
}
