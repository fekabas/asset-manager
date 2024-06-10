using WebAPI.Model.Entities;
namespace WebAPI.DataAccess.EntityFrameworkCore;
public class FileMetadataRepository : EntityFrameworkRepository<FileMetadata, FileMetadataDbContext>, IFileMetadataRepository
{
    public FileMetadataRepository(FileMetadataDbContext dbContext) : base(dbContext)
    {
    }
}