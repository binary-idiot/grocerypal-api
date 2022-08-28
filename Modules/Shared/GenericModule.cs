using System.Net;
using GroceryPalAPI.Domain;

namespace GroceryPalAPI.Modules.Shared
{
    internal abstract class GenericModule<TModelType, TRepositoryType> : IModule
        where TModelType : BaseModel
        where TRepositoryType : IRepository<TModelType>
    {
        protected abstract string BaseEndpoint { get; }

        public abstract IServiceCollection RegisterModule(IServiceCollection services);

        public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet(BaseEndpoint, GetAll);
            endpoints.MapGet($"{BaseEndpoint}/{{id}}", Get);
            endpoints.MapPost(BaseEndpoint, Post);  
            endpoints.MapDelete($"{BaseEndpoint}/{{id}}", Delete);

            return endpoints;
        }
        
        internal virtual async Task<IEnumerable<TModelType>> GetAll(TRepositoryType repository)
        {
            return await repository.FindAll();
        }

        internal virtual async Task<TModelType?> Get(Guid id, TRepositoryType repository)
        {
            TModelType? entity = await repository.Find(id);

            if (entity is null)
            {
                throw new BadHttpRequestException("item not found",
                    (int)HttpStatusCode.NotFound);
            }

            return entity;
        }
        internal virtual async Task<string> Post(HttpRequest req, HttpResponse resp, TRepositoryType repository)
        {
            if (!req.HasJsonContentType())
            {
                throw new BadHttpRequestException("only application/json supported",
                    (int)HttpStatusCode.NotAcceptable);
            }

            TModelType? entity;

            try
            {
                entity = await req.ReadFromJsonAsync<TModelType>();
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

            string id = await repository.Add(entity);
            resp.StatusCode = (int)HttpStatusCode.Created;

            return id;
        }

        internal virtual async Task Delete(Guid id, TRepositoryType repository)
        {
            bool success = await repository.Remove(id);

            if (!success)
            {
                throw new BadHttpRequestException("entity not found",
                    (int)HttpStatusCode.NotFound);
            }
        }
    }
}
