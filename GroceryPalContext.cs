using GroceryPalAPI.Modules.Item;
using Microsoft.EntityFrameworkCore;

namespace GroceryPalAPI
{
    public class GroceryPalContext : DbContext
    {
        public GroceryPalContext(DbContextOptions<GroceryPalContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
    }
}
