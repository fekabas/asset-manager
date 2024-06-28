namespace WebAPI.DataAccess.EntityFrameworkCore;

public interface IFileMetadataDbContextConfiguration
{
    string ConnectionString { get; set; }   
}