using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryPalAPI.Domain;

[Table("Items")]
public class ItemModel : BaseModel
{
    public string Name { get; set; }
}