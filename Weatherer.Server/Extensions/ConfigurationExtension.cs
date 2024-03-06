namespace Weatherer.Server.Extensions;

public static class ConfigurationExtensions
{
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        IConfigurationSection? section = configuration.GetSection(sectionName);
        var options = new T();
        section.Bind(options);

        return options;
    }

    public static T GetRequiredOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        IConfigurationSection? section = configuration.GetRequiredSection(sectionName);
        var options = new T();
        section.Bind(options);

        return options;
    }
}
