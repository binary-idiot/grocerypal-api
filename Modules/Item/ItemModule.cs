using GroceryPalAPI.Domain;
using GroceryPalAPI.Modules.Shared;

namespace GroceryPalAPI.Modules.Item;

internal class ItemModule : GenericModule<ItemModel, ItemRepository>
{
    protected override string BaseEndpoint => "/items";
    public override IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddScoped<ItemRepository>();
        return services;
    }
}