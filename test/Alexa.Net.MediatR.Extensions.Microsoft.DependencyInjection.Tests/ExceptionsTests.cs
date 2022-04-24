using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alexa.Net.MediatR.Options;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Alexa.Net.MediatR.Extensions.Microsoft.DependencyInjection.Tests;

public class ExceptionsTests
{
    [Fact]
    public void AddSkillMediatorFromAssemblies_WithNullServices_ThrowsException()
    {
        var services = (IServiceCollection)null;

        Action act = () =>
            services.AddSkillMediatorFromAssemblies(Enumerable.Empty<Assembly>(), GetDefaultConfiguration(), "");

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddSkillMediatorFromAssemblies_WithNullConfiguration_ThrowsException()
    {
        var services = GetDefaultServiceCollection();

        Action act = () => services.AddSkillMediatorFromAssemblies(Enumerable.Empty<Assembly>(), null, "");
        
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddSkillMediatorFromAssemblies_WithNullSettingsAction_ThrowsException()
    {
        var services = GetDefaultServiceCollection();

        Action act = () =>
            services.AddSkillMediatorFromAssemblies(Enumerable.Empty<Assembly>(), (Action<AlexaSkillOptions>)null);
        
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddSkillMediatorFromAssemblies_WithNullConfigSection_ThrowsException()
    {
        var services = GetDefaultServiceCollection();

        Action act = () =>
            services.AddSkillMediatorFromAssemblies(Enumerable.Empty<Assembly>(), (IConfigurationSection)null);
        
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddSkillMediatorFromAssemblies_WithEmptyAssemblies_ThrowsException()
    {
        var services = GetDefaultServiceCollection();
        var configuration = GetDefaultConfiguration();

        Action act = () => services.AddSkillMediatorFromAssemblies(Enumerable.Empty<Assembly>(), configuration, "");
        
        act.Should().Throw<ArgumentException>();
    }

    private static IServiceCollection GetDefaultServiceCollection()
    {
        var services = new ServiceCollection();
        return services;
    }
    private static IConfiguration GetDefaultConfiguration()
    {
        var configuration = new Dictionary<string, string>();
        return new ConfigurationBuilder().AddInMemoryCollection(configuration).Build();
    }
}