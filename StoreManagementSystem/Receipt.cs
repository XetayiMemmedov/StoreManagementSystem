namespace StoreManagment;

internal class Receipt : BaseModel
{
    private static int AutoIncremendId = 1;

    public Receipt(Seller seller, BuyItem[] buyItems)
    {
        Seller = seller;
        BuyItems = buyItems;
        Id = AutoIncremendId;
    }

    public Seller Seller { get; set; }
    public BuyItem[] BuyItems { get; set; }
}