using GroceryPalAPI.Modules.Shared;
using Microsoft.EntityFrameworkCore;

namespace GroceryPalAPI.Modules.Item
{
    internal class ItemRepository: IRepository<Item>
    {

        private readonly GroceryPalContext _context;
        public ItemRepository(GroceryPalContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }

        public async Task<IEnumerable<Item>> FindAll()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item?> Find(Guid id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<string> Add(Item entity)
        {
            await _context.Items.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id.ToString();
        }

        public Task<Item?> Update(Item entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(Guid id)
        {
            Item? item = await _context.Items.FindAsync(id);

            if (item is not null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

    }
}
