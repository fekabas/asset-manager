using WebAPI.Model;

namespace WebAPI.DataAccess.EntityFrameworkCore;
public interface IEntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity
{
    Task DeleteAsync(Guid id);
}