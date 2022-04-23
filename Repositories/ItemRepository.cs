using GroceryPalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryPalAPI.Repositories
{
    internal class ItemRepository: IRepository<Item, GroceryPalContext>
    {

        //private readonly Dictionary<string, Item> items = new Dictionary<string, Item>();

        public async Task<IEnumerable<Item>> FindAll(GroceryPalContext db)
        {
            return await db.Items.ToListAsync();
        }

        public async Task<Item> Find(GroceryPalContext db,string id)
        {
            return await db.Items.FindAsync(id);
        }

        public async Task<string> Add(GroceryPalContext db, Item entity)
        {
            entity.ItemId = Guid.NewGuid().ToString("N");

            await db.Items.AddAsync(entity);
            await db.SaveChangesAsync();

            return entity.ItemId;
        }

        public Task<Item> Update(GroceryPalContext db, Item entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(GroceryPalContext db, string id)
        {
            Item item = await db.Items.FindAsync(id);
            if (item is null)
            {
                return false;
            }
            db.Items.Remove(item);
            await db.SaveChangesAsync();

            return true;
        }

    }
}
