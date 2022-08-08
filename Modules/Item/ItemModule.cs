using GroceryPalAPI.Modules.Shared;

namespace GroceryPalAPI.Modules.Item;

internal class ItemModule : IModule
{
    private readonly string _baseEndpoint = "/items";

    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddScoped<ItemRepository>();

        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(_baseEndpoint, GenericEndpoints<Item, ItemRepository>.GetAll);
        endpoints.MapGet($"{_baseEndpoint}/{{id}}", GenericEndpoints<Item, ItemRepository>.Get);
        endpoints.MapPost(_baseEndpoint, GenericEndpoints<Item, ItemRepository>.Post);  
        endpoints.MapDelete($"{_baseEndpoint}/{{id}}", GenericEndpoints<Item, ItemRepository>.Delete);

        return endpoints;
    }
}