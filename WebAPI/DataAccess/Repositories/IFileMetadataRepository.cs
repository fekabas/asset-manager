using WebAPI.DataAccess.EntityFrameworkCore;
using WebAPI.Model.Entities;
namespace WebAPI.DataAccess;
public interface IFileMetadataRepository : IEntityFrameworkRepository<FileMetadata>
{

}