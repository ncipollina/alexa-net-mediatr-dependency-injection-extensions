using System.Reflection;
using Alexa.Net.MediatR.Options;
using Alexa.Net.MediatR.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Alexa.Net.MediatR;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSkillMediatorFromAssemblies(this IServiceCollection services, IEnumerable<Assembly> assemblies, IConfiguration configuration,
        string sectionName)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        var settings = new AlexaSkillOptions();
        configuration.Bind(sectionName, settings);
        return services.AddSkillMediator(assemblies, settings);
    }

    public static IServiceCollection AddSkillMediatorFromAssemblies(this IServiceCollection services, IEnumerable<Assembly> assemblies,
        Action<AlexaSkillOptions> settingsAction)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));
        if (settingsAction is null)
            throw new ArgumentNullException(nameof(settingsAction));

        var settings = new AlexaSkillOptions();
        settingsAction.Invoke(settings);
        return services.AddSkillMediator(assemblies, settings);
    }

    public static IServiceCollection AddSkillMediatorFromAssemblies(this IServiceCollection services, IEnumerable<Assembly> assemblies,
        IConfigurationSection configSection) 
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));
        if (configSection is null)
            throw new ArgumentNullException(nameof(configSection));

        var settings = new AlexaSkillOptions();
        configSection.GetSection(configSection.Key).Bind(settings);
        return services.AddSkillMediator(assemblies, settings);
    }

    public static IServiceCollection AddSkillMediatorFromAssemblies(this IServiceCollection services,
        IConfiguration configuration,
        string sectionName, params Assembly[] assemblies) =>
        services.AddSkillMediatorFromAssemblies(assemblies, configuration, sectionName);

    public static IServiceCollection AddSkillMediatorFromAssemblies(this IServiceCollection services,
        Action<AlexaSkillOptions> settingsAction, params Assembly[] assemblies) =>
        services.AddSkillMediatorFromAssemblies(assemblies, settingsAction);

    public static IServiceCollection AddSkillMediatorFromAssemblies(this IServiceCollection services,
        IConfigurationSection configSection, params Assembly[] assemblies) =>
        services.AddSkillMediatorFromAssemblies(assemblies, configSection);
    
    public static IServiceCollection AddSkillMediatorFromAssembly(this IServiceCollection services,
        IConfiguration configuration,
        string sectionName, Assembly assembly) =>
        services.AddSkillMediatorFromAssemblies(configuration, sectionName, assembly);

    public static IServiceCollection AddSkillMediatorFromAssembly(this IServiceCollection services,
        Action<AlexaSkillOptions> settingsAction, Assembly assembly) =>
        services.AddSkillMediatorFromAssemblies(settingsAction, assembly);

    public static IServiceCollection AddSkillMediatorFromAssembly(this IServiceCollection services,
        IConfigurationSection configSection, Assembly assembly) =>
        services.AddSkillMediatorFromAssemblies(configSection, assembly);

    public static IServiceCollection AddSkillMediatorFromAssembliesContaining(this IServiceCollection services,
        IEnumerable<Type> assemblyTypes, IConfiguration configuration,
        string sectionName) =>
        services.AddSkillMediatorFromAssemblies(assemblyTypes.Select(t => t.GetTypeInfo().Assembly), configuration,
            sectionName);

    public static IServiceCollection AddSkillMediatorFromAssembliesContaining(this IServiceCollection services,
        IEnumerable<Type> assemblyTypes,
        Action<AlexaSkillOptions> settingsAction) =>
        services.AddSkillMediatorFromAssemblies(assemblyTypes.Select(t => t.GetTypeInfo().Assembly), settingsAction);

    public static IServiceCollection AddSkillMediatorFromAssembliesContaining(this IServiceCollection services,
        IEnumerable<Type> assemblyTypes,
        IConfigurationSection configSection) =>
        services.AddSkillMediatorFromAssemblies(assemblyTypes.Select(t => t.GetTypeInfo().Assembly), configSection);

    public static IServiceCollection AddSkillMediatorFromAssembliesContaining(this IServiceCollection services,
        IConfiguration configuration, string sectionName, params Type[] assemblyTypes) =>
        services.AddSkillMediatorFromAssembliesContaining(assemblyTypes, configuration,
            sectionName);

    public static IServiceCollection AddSkillMediatorFromAssembliesContaining(this IServiceCollection services,
        Action<AlexaSkillOptions> settingsAction, params Type[] assemblyTypes) =>
        services.AddSkillMediatorFromAssembliesContaining(assemblyTypes, settingsAction);

    public static IServiceCollection AddSkillMediatorFromAssembliesContaining(this IServiceCollection services,
        IConfigurationSection configSection, params Type[] assemblyTypes) =>
        services.AddSkillMediatorFromAssembliesContaining(assemblyTypes, configSection);

    public static IServiceCollection AddSkillMediatorFromAssemblyContaining(this IServiceCollection services,
        IConfiguration configuration, string sectionName, Type assemblyType) =>
        services.AddSkillMediatorFromAssembliesContaining(configuration,
            sectionName,assemblyType);

    public static IServiceCollection AddSkillMediatorFromAssemblyContaining(this IServiceCollection services,
        Action<AlexaSkillOptions> settingsAction, Type assemblyType) =>
        services.AddSkillMediatorFromAssembliesContaining(settingsAction, assemblyType);

    public static IServiceCollection AddSkillMediatorFromAssemblyContaining(this IServiceCollection services,
        IConfigurationSection configSection, Type assemblyType) =>
        services.AddSkillMediatorFromAssembliesContaining(configSection, assemblyType);

    public static IServiceCollection AddSkillMediatorFromAssemblyContaining<T>(this IServiceCollection services,
        IConfiguration configuration, string sectionName) =>
        services.AddSkillMediatorFromAssembliesContaining(configuration,
            sectionName,typeof(T));

    public static IServiceCollection AddSkillMediatorFromAssemblyContaining<T>(this IServiceCollection services,
        Action<AlexaSkillOptions> settingsAction) =>
        services.AddSkillMediatorFromAssembliesContaining(settingsAction, typeof(T));

    public static IServiceCollection AddSkillMediatorFromAssemblyContaining<T>(this IServiceCollection services,
        IConfigurationSection configSection) =>
        services.AddSkillMediatorFromAssembliesContaining(configSection, typeof(T));

    private static IServiceCollection AddSkillMediator(this IServiceCollection services,
        IEnumerable<Assembly> assemblies, AlexaSkillOptions settings)
    {
        if (!assemblies.Any())
            throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");

        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AlexaSkillOptions>>().Value);
        services.AddSingleton(settings);
        services.AddRequiredServices();
        services.AddSkillMediatorClasses(assemblies);
        
        return services;
    }
}