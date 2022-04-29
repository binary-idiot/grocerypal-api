using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GroceryPalAPI.Endpoints
{
    internal class GenericEndpoints<ModelType, RepositoryType> 
        where RepositoryType : GroceryPalAPI.Repositories.IRepository<ModelType>
    {
        internal static async Task<IEnumerable<ModelType>> GetAll(RepositoryType repo)
        {
            return await repo.FindAll();
        }

        internal static async Task<ModelType?> Get(Guid id, RepositoryType repo)
        {

            ModelType? entity = await repo.Find(id);

            if (entity is null)
            {
                throw new BadHttpRequestException("item not found",
                    (int)HttpStatusCode.NotFound);
            }

            return entity;
        }
        internal static async Task<string> Post(HttpRequest req, HttpResponse resp, RepositoryType repo)
        {
            if (!req.HasJsonContentType())
            {
                throw new BadHttpRequestException("only application/json supported",
                    (int)HttpStatusCode.NotAcceptable);
            }

            ModelType? entity;

            try
            {
                entity = await req.ReadFromJsonAsync<ModelType>();
            }
            catch (Exception ex)
            {

                throw;
            }

            if (entity is null /*|| string.IsNullOrWhiteSpace(entity.name)*/)
            {
                throw new BadHttpRequestException("entity cannot be null",
                    (int)HttpStatusCode.BadRequest);
            }

            string id = await repo.Add(entity);
            resp.StatusCode = (int)HttpStatusCode.Created;

            return id;
        }

        internal static async Task Delete(Guid id, RepositoryType repo)
        {
            bool success = await repo.Remove(id);

            if (!success)
            {
                throw new BadHttpRequestException("entity not found",
                    (int)HttpStatusCode.NotFound);
            }
        }
    }
}
