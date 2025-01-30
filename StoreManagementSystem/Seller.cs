using System;

namespace StoreManagment;

internal class Seller : Person
{
    private static int AutoIncremendId = 1;

    public Seller(string name, int storeId) : base(name)
    {
        StoreId = storeId;
        Id = AutoIncremendId++;
    }

    public int StoreId { get; set; }
}
