using GroceryPalAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace GroceryPalAPI.Modules.Shared;

internal abstract class GenericRepository<TModelType> : IRepository<TModelType>
	where TModelType : BaseModel
{
	protected readonly GroceryPalContext _context;
	protected DbSet<TModelType> table;

	internal GenericRepository(GroceryPalContext context)
	{
		_context = context ?? throw new ArgumentNullException("context");
		table = _context.Set<TModelType>();
	}
	
	public virtual async Task<IEnumerable<TModelType>> FindAll()
	{
		return await table.ToListAsync();
	}

	public virtual async Task<TModelType?> Find(Guid id)
	{
		return await table.FindAsync(id);
	}

	public virtual async Task<string> Add(TModelType entity)
	{
		await table.AddAsync(entity);
		await _context.SaveChangesAsync();

		return entity.Id.ToString();
	}

	public virtual Task<TModelType?> Update(TModelType entity)
	{
		throw new NotImplementedException();
	}

	public virtual async Task<bool> Remove(Guid id)
	{
		TModelType? entity = await table.FindAsync(id);

		if (entity is not null)
		{
			table.Remove(entity);
			await _context.SaveChangesAsync();

			return true;
		}

		return false;
	}
}