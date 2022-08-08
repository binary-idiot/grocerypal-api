namespace GroceryPalAPI.Modules;

public interface IModule
{
	IServiceCollection RegisterModule(IServiceCollection services);
	IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}