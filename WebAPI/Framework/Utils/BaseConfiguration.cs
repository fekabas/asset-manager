using Microsoft.Extensions.Primitives;

namespace WebAPI.Framework.Utils;
/// <summery>
/// Abstract class to implement configuration building.
/// Every implementation should declare a function from where to get the configuration values
/// and the properties that need to be initialized with that configuration
/// </summery>
public abstract class BaseConfiguration : IConfiguration
{
    public BaseConfiguration(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;

        this.configurationRoot = this.configurationRootProvider(this.serviceProvider);

        configurationRoot.Bind(this);
    }
    protected readonly IServiceProvider serviceProvider;
    protected readonly IConfigurationRoot configurationRoot;
    protected abstract Func<IServiceProvider,IConfigurationRoot> configurationRootProvider { get; set; }

    public string? this[string key]
    {
        get
        {
            return configurationRoot[key];
        }

        set
        {
            configurationRoot[key] = value;
            if (value == null) configurationRoot.Reload();
        }
    }

    public virtual IEnumerable<IConfigurationSection> GetChildren()
    {
        return configurationRoot.GetChildren();
    }

    public virtual IChangeToken GetReloadToken()
    {
        return configurationRoot.GetReloadToken();
    }

    public virtual IConfigurationSection GetSection(string key)
    {
        return configurationRoot.GetSection(key);
    }
}