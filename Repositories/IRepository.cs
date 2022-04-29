using Microsoft.EntityFrameworkCore;

namespace GroceryPalAPI.Repositories
{
    internal interface IRepository<ModelType>
    {
        Task<IEnumerable<ModelType>> FindAll();
        Task<ModelType?> Find(Guid id);
        Task<string> Add(ModelType entity);
        Task<ModelType?> Update(ModelType entity);
        Task<bool> Remove(Guid id);
    }
}
