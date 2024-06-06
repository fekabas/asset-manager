using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.DataAccess.EntityFrameworkCore;
public class FileMetadataDbContext : DbContext
{
    public FileMetadataDbContext(DbContextOptions options)
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