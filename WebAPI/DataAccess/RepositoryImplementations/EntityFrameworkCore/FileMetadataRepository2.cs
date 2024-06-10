using WebAPI.Model.Entities;
namespace WebAPI.DataAccess.EntityFrameworkCore;
public class FileMetadataRepository2 : EntityFrameworkRepository<FileMetadata, FileMetadataDbContext2>, IFileMetadataRepository2
{
    public FileMetadataRepository2(FileMetadataDbContext2 dbContext) : base(dbContext)
    {
    }
}