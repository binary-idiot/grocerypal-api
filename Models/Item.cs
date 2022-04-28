namespace GroceryPalAPI.Models;

public class Item : BaseModel
{
    public string Name { get; set; }

    public Item(Guid? Id, string LocalId, string Name) : base(Id, LocalId)
    {
        this.Name = Name;
    }
}