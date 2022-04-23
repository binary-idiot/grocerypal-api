using GroceryPalAPI;
using GroceryPalAPI.Endpoints;
using GroceryPalAPI.Repositories;
using Microsoft.EntityFrameworkCore;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
#if DEBUG
    .AddUserSecrets("dbb48030-81bc-450e-b3e0-a080017d30dd")
    .AddJsonFile("appsettings.Development.json")
#endif
    .AddEnvironmentVariables()
    .Build();

string DB_CONNECTION_STRING =
    $"Server={config["Database:Server"]};Port={config["Database:Port"]};Database={config["Database:Name"]};Username={config["DB_USER"]};Password={config["DB_PASSWORD"]};";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddDbContext<GroceryPalContext>(options => options.UseNpgsql(DB_CONNECTION_STRING));

builder.Services.AddSingleton<ItemRepository>();


WebApplication app = builder.Build();

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


app.RegisterItemEndpoints();

app.Run();
