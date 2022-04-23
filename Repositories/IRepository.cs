using Microsoft.EntityFrameworkCore;

namespace GroceryPalAPI.Repositories
{
    internal interface IRepository<ModelType, ContextType> where ContextType : DbContext
    {
        Task<IEnumerable<ModelType>> FindAll(ContextType db);
        Task<ModelType> Find(ContextType db, string id);
        Task<string> Add(ContextType db, ModelType entity);
        Task<ModelType> Update(ContextType db, ModelType entity);
        Task<bool> Remove(ContextType db, string id);
    }
}
