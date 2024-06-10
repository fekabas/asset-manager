using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess.EntityFrameworkCore;

namespace WebAPI.DataAccess;

internal static class DataAccessConfigurer
{
    /// <summary>
    /// Configure WebApp by adding base layers and injecting WebApp's specific services.
    /// </summary>
    public static IServiceCollection ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        // We need to define a way to access data.
        // We may encounter that there are more than one database we are accessing.
        // This means multiple DbContexts.
        // If done well, we would need just a way to resolve the given repository when instantiating a repository.
        // For example we would declare a repository for FileMetadata using the FileMetadataDbcontext like this:
        // private readonly EntityFrameworkRepository<FileMetadata, FileMetadataDbContext> fileMetadataRepository;
        // And then use this repository to manage FileMetadata transactions.


        // Register the multiple DbContexts and map them to different each one of it's interfaces
        // So that they can be resolved by the providers
        // #### CHALLENGE THIS ####
        // services.AddScoped<DbContext, FileMetadataDbContext>();
        // services.AddScoped<DbContext, AuthenticationDbContext>();

        // Define the providers for each one of those DbContexts
        //services.AddDbContext<FileMetadataDbContext>((options) => options.UseSqlServer(configuration.GetConnectionString("FileMetadata")), ServiceLifetime.Scoped);
        services.AddDbContext<FileMetadataDbContext>((options) => options.UseInMemoryDatabase("FileMetadataDbContext"));

        // Register all repositories
        services.AddScoped<IFileMetadataRepository, FileMetadataRepository>();
        
        // Demo repo and context
        // services.AddDbContext<FileMetadataDbContext2>((options) => options.UseInMemoryDatabase("AssetManagerFileMetadataContext2"));
        // services.AddScoped<IFileMetadataRepository2, FileMetadataRepository2>();

        return services;
    }
}