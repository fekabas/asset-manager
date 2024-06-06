using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.DataAccess.EntityFrameworkCore;

public class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly FileMetadataDbContext dbContext;
    public EntityFrameworkRepository(FileMetadataDbContext dbContext)
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
}
