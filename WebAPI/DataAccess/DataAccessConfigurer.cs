using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess.EntityFrameworkCore;

namespace WebAPI.DataAccess;

internal static class DataAccessConfigurer
{
    private static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
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
        // services.AddDbContext<FileMetadataDbContext>((options) => options.UseSqlServer(configuration.GetConnectionString("FileMetadata")), ServiceLifetime.Scoped);
        // services.AddDbContext<FileMetadataDbContext>((options) => options.UseInMemoryDatabase("FileMetadataDbContext"));

        // Register File Metadata dB context configuration
        var serviceProvider = services.BuildServiceProvider();
        services.AddSingleton<IFileMetadataDbContextConfiguration, FileMetadataDbContextConfiguration>(provider => new FileMetadataDbContextConfiguration(services.BuildServiceProvider()));
        var fileMetadataDbContextConfiguration = serviceProvider.GetRequiredService<IFileMetadataDbContextConfiguration>();

        // Configure Dbcontext
        string fileMetadataConnectionString = fileMetadataDbContextConfiguration.ConnectionString;
        services.AddDbContext<FileMetadataDbContext>((options) => options.UseNpgsql(fileMetadataConnectionString), ServiceLifetime.Scoped);

        // Register all repositories
        services.AddScoped<IFileMetadataRepository, FileMetadataRepository>();

        return services;
    }
    /// <summary>
    /// Configure WebApp by adding base layers and injecting WebApp's specific services.
    /// </summary>
    public static IServiceCollection ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    => Configure(services, configuration, env);
}