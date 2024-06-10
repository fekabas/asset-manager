using Microsoft.EntityFrameworkCore;
using WebAPI.Model.Entities;

namespace WebAPI.DataAccess.EntityFrameworkCore;
public class FileMetadataDbContext2 : DbContext
{
    public FileMetadataDbContext2(DbContextOptions<FileMetadataDbContext2> options)
    : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<FileMetadata> FilesMetadata { get; set; }
}