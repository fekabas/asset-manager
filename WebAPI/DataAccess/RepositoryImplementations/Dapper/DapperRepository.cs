using WebAPI.Model;

namespace WebAPI.DataAccess.Dapper;
public class DapperRepository<T> : IRepository<T> where T : class, IEntity
{
    public async Task AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<T?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}