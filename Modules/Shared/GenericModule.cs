using System.Net;

namespace GroceryPalAPI.Modules.Shared
{
    internal abstract class GenericModule<ModelType, RepositoryType> : IModule
        where ModelType : BaseModel
        where RepositoryType : GenericRepository<ModelType>
    {
        protected abstract string BaseEndpoint { get; }

        public virtual IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<RepositoryType>();
            return services;
        }

        public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet(BaseEndpoint, GetAll);
            endpoints.MapGet($"{BaseEndpoint}/{{id}}", Get);
            endpoints.MapPost(BaseEndpoint, Post);  
            endpoints.MapDelete($"{BaseEndpoint}/{{id}}", Delete);

            return endpoints;
        }
        
        internal virtual async Task<IEnumerable<ModelType>> GetAll(RepositoryType repository)
        {
            return await repository.FindAll();
        }

        internal virtual async Task<ModelType?> Get(Guid id, RepositoryType repository)
        {
            ModelType? entity = await repository.Find(id);

            if (entity is null)
            {
                throw new BadHttpRequestException("item not found",
                    (int)HttpStatusCode.NotFound);
            }

            return entity;
        }
        internal virtual async Task<string> Post(HttpRequest req, HttpResponse resp, RepositoryType repository)
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

            string id = await repository.Add(entity);
            resp.StatusCode = (int)HttpStatusCode.Created;

            return id;
        }

        internal virtual async Task Delete(Guid id, RepositoryType repository)
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
