using GroceryPalAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace GroceryPalAPI.Modules.Shared;

internal abstract class GenericRepository<ModelType> : IRepository<ModelType>
	where ModelType : BaseModel
{
	protected readonly GroceryPalContext _context;
	protected DbSet<ModelType> table;

	internal GenericRepository(GroceryPalContext context)
	{
		_context = context ?? throw new ArgumentNullException("context");
		table = _context.Set<ModelType>();
	}
	
	public virtual async Task<IEnumerable<ModelType>> FindAll()
	{
		return await table.ToListAsync();
	}

	public virtual async Task<ModelType?> Find(Guid id)
	{
		return await table.FindAsync(id);
	}

	public virtual async Task<string> Add(ModelType entity)
	{
		await table.AddAsync(entity);
		await _context.SaveChangesAsync();

		return entity.Id.ToString();
	}

	public virtual Task<ModelType?> Update(ModelType entity)
	{
		throw new NotImplementedException();
	}

	public virtual async Task<bool> Remove(Guid id)
	{
		ModelType? entity = await table.FindAsync(id);

		if (entity is not null)
		{
			table.Remove(entity);
			await _context.SaveChangesAsync();

			return true;
		}

		return false;
	}
}