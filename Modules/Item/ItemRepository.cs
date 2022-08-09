using GroceryPalAPI.Domain;
using GroceryPalAPI.Modules.Shared;

namespace GroceryPalAPI.Modules.Item
{
    internal class ItemRepository: GenericRepository<ItemModel>
    {
        public ItemRepository(GroceryPalContext context) : base(context) { }
    }
}
