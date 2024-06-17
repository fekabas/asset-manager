using System;
using Microsoft.Extensions.Configuration;
using WebAPI.Framework.Utils;

namespace WebAPI.DataAccess.EntityFrameworkCore;
public class FileMetadataDbContextConfiguration : BaseConfiguration, IFileMetadataDbContextConfiguration
{
    public string ConnectionString { get; set; }
    public FileMetadataDbContextConfiguration(ServiceProvider serviceProvider) : base(serviceProvider)
    {
        
    }

    protected override Func<IServiceProvider, IConfigurationRoot> configurationRootProvider
    {
        get =>
            (serviceProvider) =>
            {
                var storageConfigurationBuilder = new ConfigurationBuilder();

                // Add default configuration defined in json file.
                storageConfigurationBuilder
                    .AddJsonFile("./DataAccess/RepositoryImplementations/EntityFrameworkCore/Configuration/dataAccessConfiguration.json", optional: false, reloadOnChange: true);

                // Add variables form current environment
                storageConfigurationBuilder.AddEnvironmentVariables();

                // If this is development, load development configuration file
                var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                    storageConfigurationBuilder.AddJsonFile("./DataAccess/RepositoryImplementations/EntityFrameworkCore/Configuration/dataAccessConfiguration.Development.json", optional: true, reloadOnChange: true);

                return storageConfigurationBuilder.Build();
            };
        set => configurationRootProvider = value;
    }
}