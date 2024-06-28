using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.DataAccess.EntityFrameworkCore;

public class EntityFrameworkRepository<T, DT> : IEntityFrameworkRepository<T> where T : class, IEntity where DT : DbContext
{
    private readonly DT dbContext;
    public EntityFrameworkRepository(DT dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public IQueryable<T> GetAll() => dbContext.Set<T>();

    public async Task<T?> GetAsync(Guid id) => await GetAll().FirstOrDefaultAsync(e => e.Id == id);

    public async Task UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetAsync(id);
        await DeleteAsync(entity!);
    }
}
