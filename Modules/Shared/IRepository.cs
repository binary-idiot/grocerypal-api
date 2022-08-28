namespace GroceryPalAPI.Modules.Shared
{
    internal interface IRepository<TModelType>
    {
        Task<IEnumerable<TModelType>> FindAll();
        Task<TModelType?> Find(Guid id);
        Task<string> Add(TModelType entity);
        Task<TModelType?> Update(TModelType entity);
        Task<bool> Remove(Guid id);
    }
}
