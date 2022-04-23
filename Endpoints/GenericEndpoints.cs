using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GroceryPalAPI.Endpoints
{
    internal class GenericEndpoints<ModelType, RepositoryType, ContextType> 
        where RepositoryType : GroceryPalAPI.Repositories.IRepository<ModelType, ContextType> 
        where ContextType : DbContext
    {
        internal static async Task<IEnumerable<ModelType>> GetAll(ContextType db, RepositoryType repo)
        {
            return await repo.FindAll(db);
        }

        internal static async Task<ModelType> Get(ContextType db, string id, RepositoryType repo)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new BadHttpRequestException("id is required",
                    (int)HttpStatusCode.BadRequest);
            }

            ModelType entity = await repo.Find(db, id);

            if (entity == null)
            {
                throw new BadHttpRequestException("item not found",
                    (int)HttpStatusCode.NotFound);
            }

            return entity;
        }
        internal static async Task<string> Post(HttpRequest req, HttpResponse resp, ContextType db, RepositoryType repo)
        {
            if (!req.HasJsonContentType())
            {
                throw new BadHttpRequestException("only application/json supported",
                    (int)HttpStatusCode.NotAcceptable);
            }

            var entity = await req.ReadFromJsonAsync<ModelType>();

            if (entity == null /*|| string.IsNullOrWhiteSpace(entity.name)*/)
            {
                throw new BadHttpRequestException("entity cannot be null",
                    (int)HttpStatusCode.BadRequest);
            }

            var id = await repo.Add(db, entity);
            resp.StatusCode = (int)HttpStatusCode.Created;

            return id;
        }

        internal static async void Delete(ContextType db, string id, RepositoryType repo)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new BadHttpRequestException("id is required",
                  (int)HttpStatusCode.BadRequest);
            }

            var success = await repo.Remove(db, id);

            if (!success)
            {
                throw new BadHttpRequestException("entity not found",
                    (int)HttpStatusCode.NotFound);
            }
        }
    }
}
