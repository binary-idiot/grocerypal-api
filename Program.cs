using GroceryPalAPI;
using GroceryPalAPI.Endpoints;
using GroceryPalAPI.Repositories;
using Microsoft.EntityFrameworkCore;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

string DB_CONNECTION_STRING =
    $"Host={config.GetSection("Database:Host")};Port={config.GetSection("Database:Port")};Database={config.GetSection("Database:Name")};Username={config["DB_USER"]};Password={config["DB_PASSWORD"]}";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddDbContext<GroceryPalContext>(options => options.UseNpgsql(DB_CONNECTION_STRING));

builder.Services.AddSingleton<ItemRepository>();


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (BadHttpRequestException ex)
    {
        ctx.Response.StatusCode = ex.StatusCode;
        await ctx.Response.WriteAsync(ex.Message);
    }
});

app.RegisterItemEndpoints();

app.Run();
