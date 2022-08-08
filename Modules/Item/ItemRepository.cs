using GroceryPalAPI.Modules.Shared;
using Microsoft.EntityFrameworkCore;

namespace GroceryPalAPI.Modules.Item
{
    internal class ItemRepository: GenericRepository<Item>
    {
        public ItemRepository(GroceryPalContext context) : base(context) { }
    }
}
