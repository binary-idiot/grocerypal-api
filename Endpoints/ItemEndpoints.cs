using GroceryPalAPI.Models;
using GroceryPalAPI.Repositories;
using System.Net;

namespace GroceryPalAPI.Endpoints;

internal static class ItemEndpoints
{
    private static readonly string _baseEndpoint = "/items";
    internal static void RegisterItemEndpoints(this WebApplication app)
    {
        app.MapGet(_baseEndpoint, GetItems);
        app.MapGet($"{_baseEndpoint}/{{id}}", GetItem);
        app.MapPost(_baseEndpoint, PostItem);  
        app.MapDelete($"{_baseEndpoint}/{{id}}", DeleteItem);
    }

    private static async Task<IEnumerable<Item>> GetItems(ItemRepository repo)
    {
        return await repo.GetAll();
    }

    private static async Task<Item> GetItem(string id, ItemRepository repo)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new BadHttpRequestException("id is required",
                (int)HttpStatusCode.BadRequest);
        }

        Item item = await repo.Get(id);

        if (item == null)
        {
            throw new BadHttpRequestException("item not found",
                (int)HttpStatusCode.NotFound);
        }

        return item;
    }

    private static async Task<string> PostItem(HttpRequest req, HttpResponse resp, ItemRepository repo)
    {
        if (!req.HasJsonContentType())
        {
            throw new BadHttpRequestException("only application/json supported",
                (int)HttpStatusCode.NotAcceptable);
        }

        var item = await req.ReadFromJsonAsync<Item>();

        if (item == null || string.IsNullOrWhiteSpace(item.name))
        {
            throw new BadHttpRequestException("name is required",
                (int)HttpStatusCode.BadRequest);
        }

        var id = await repo.Create(item.name);
        resp.StatusCode = (int)HttpStatusCode.Created;
        resp.Headers.Location = $"/items/{id}";

        return id;
    }

    private static async void DeleteItem(string id, ItemRepository repo)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new BadHttpRequestException("id is required",
              (int)HttpStatusCode.BadRequest);
        }

        var success = await repo.Delete(id);

        if (!success)
        {
            throw new BadHttpRequestException("item not found",
                (int)HttpStatusCode.NotFound);
        }
    }
}