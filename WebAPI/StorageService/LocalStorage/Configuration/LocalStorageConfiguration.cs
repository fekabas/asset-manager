using WebAPI.Framework.Utils;

namespace WebAPI.StorageService.LocalStorage;
public class LocalStorageConfiguration : BaseConfiguration, ILocalStorageConfiguration
{
    public string VolumeStoragePath { get; set; }
    protected override Func<IServiceProvider, IConfigurationRoot> configurationRootProvider
    {
        get =>
            (serviceProvider) =>
            {
                var storageConfigurationBuilder = new ConfigurationBuilder();

                // Add default configuration defined in json file.
                storageConfigurationBuilder
                    .AddJsonFile("./StorageService/LocalStorage/Configuration/localStorageConfiguration.json", optional: false, reloadOnChange: true);

                // Add variables form current environment
                storageConfigurationBuilder.AddEnvironmentVariables();

                // If this is development, load development configuration file
                var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                    storageConfigurationBuilder.AddJsonFile("./StorageService/LocalStorage/Configuration/localStorageConfiguration.Development.json", optional: true, reloadOnChange: true);

                return storageConfigurationBuilder.Build();
            };
        set => configurationRootProvider = value;
    }

    public LocalStorageConfiguration(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}