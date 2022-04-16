using GroceryPalAPI.Models;
using GroceryPalAPI.Repositories;
using System.Net;

namespace GroceryPalAPI.Endpoints;

internal static class ItemEndpoints
{
    private static readonly string _baseEndpoint = "/items";
    internal static void RegisterItemEndpoints(this WebApplication app)
    {
        app.MapGet(_baseEndpoint, GenericEndpoints<Item, ItemRepository>.GetAll);
        app.MapGet($"{_baseEndpoint}/{{id}}", GenericEndpoints<Item, ItemRepository>.Get);
        app.MapPost(_baseEndpoint, GenericEndpoints<Item, ItemRepository>.Post);  
        app.MapDelete($"{_baseEndpoint}/{{id}}", GenericEndpoints<Item, ItemRepository>.Delete);
    }
}