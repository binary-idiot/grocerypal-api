namespace GroceryPalAPI.Modules;

internal interface IModule
{
	IServiceCollection RegisterModule(IServiceCollection services);
	IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}