namespace GroceryPalAPI.Repositories
{
    internal interface IRepository<ModelType>
    {
        Task<IEnumerable<ModelType>> FindAll();
        Task<ModelType> Find(string id);
        Task<string> Add(ModelType entity);
        Task<ModelType> Update(ModelType entity);
        Task<bool> Remove(string id);
    }
}
