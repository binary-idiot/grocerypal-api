using GroceryPalAPI.Models;

namespace GroceryPalAPI.Repositories
{
    public class ItemRepository
    {

        private readonly Dictionary<string, Item> items = new Dictionary<string, Item>();

        public Task<string> Create(string name)
        {
            var id = Guid.NewGuid().ToString("N");

            items.Add(id, new Item(id, name));

            return Task.FromResult(id);
        }

        public Task<bool> Delete(string id)
        {
            var success = items.Remove(id);

            return Task.FromResult(success);
        }

        public Task<Item> Get(string id)
        {
            if (items.ContainsKey(id))
                return Task.FromResult(items[id]);

            return Task.FromResult<Item>(null);
        }

        public Task<IEnumerable<Item>> GetAll()
        {
            var values = items.Select(i => i.Value);
            return Task.FromResult(values);
        }
    }
}
