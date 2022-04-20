using GroceryPalAPI.Models;

namespace GroceryPalAPI.Repositories
{
    internal class ItemRepository: IRepository<Item>
    {

        private readonly Dictionary<string, Item> items = new Dictionary<string, Item>();

        public Task<IEnumerable<Item>> FindAll()
        {
            var values = items.Select(i => i.Value);
            return Task.FromResult(values);
        }

        public Task<Item> Find(string id)
        {
            if (items.ContainsKey(id))
                return Task.FromResult(items[id]);

            return Task.FromResult<Item>(null);
        }

        public Task<string> Add(Item entity)
        {
            Item item = entity with { itemId = Guid.NewGuid().ToString("N") };

            items.Add(item.itemId, item);

            return Task.FromResult(item.itemId);
        }

        public Task<Item> Update(Item entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(string id)
        {
            var success = items.Remove(id);

            return Task.FromResult(success);
        }

    }
}
