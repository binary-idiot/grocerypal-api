using GroceryPalAPI.Models;
using GroceryPalAPI.Repositories;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/item", async (
        HttpRequest req,
        HttpResponse resp,
        ItemRepository repo) =>
{
    if (!req.HasJsonContentType())
    {
        throw new BadHttpRequestException("only application/json supported",
            (int)HttpStatusCode.NotAcceptable);
    }

    var item = await req.ReadFromJsonAsync<Item>();

    if (item != null || string.IsNullOrWhiteSpace(item.name))
    {
        throw new BadHttpRequestException("description is required",
            (int)HttpStatusCode.BadRequest);
    }

    var id = await repo.CreateAsync(item.name);
    resp.StatusCode = (int)HttpStatusCode.Created;
    resp.Headers.Location = $"/todo/{id}";
});

app.Run();
