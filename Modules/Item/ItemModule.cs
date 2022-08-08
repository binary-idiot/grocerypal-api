using GroceryPalAPI.Modules.Shared;

namespace GroceryPalAPI.Modules.Item;

internal class ItemModule : GenericModule<Item, ItemRepository>
{
    protected override string BaseEndpoint => "/items";
}