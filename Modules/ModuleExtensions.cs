namespace GroceryPalAPI.Modules;

// Based module architecture off of:
// https://timdeschryver.dev/blog/maybe-its-time-to-rethink-our-project-structure-with-dot-net-6

public static class ModuleExtensions
{
	private static readonly List<IModule> RegisteredModules = new List<IModule>();

	public static IServiceCollection RegisterModules(this IServiceCollection services)
	{
		IEnumerable<IModule> modules = DiscoverModules();
		foreach (var module in modules)
		{
			module.RegisterModule(services);
			RegisteredModules.Add(module);
		}

		return services;
	}

	public static WebApplication MapEndpoints(this WebApplication app)
	{
		foreach (var module in RegisteredModules)
		{
			module.MapEndpoints(app);
		}

		return app;
	}

	private static IEnumerable<IModule> DiscoverModules()
	{
		return typeof(IModule).Assembly
			.GetTypes()
			.Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
			.Select(Activator.CreateInstance)
			.Cast<IModule>();
	}
}