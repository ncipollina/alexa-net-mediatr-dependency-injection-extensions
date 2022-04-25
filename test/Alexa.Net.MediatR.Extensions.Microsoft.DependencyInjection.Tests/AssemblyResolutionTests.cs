using System;
using System.Collections.Generic;
using Alexa.Net.MediatR.Pipeline;
using Alexa.NET.Request.Type;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Alexa.Net.MediatR.Extensions.Microsoft.DependencyInjection.Tests;

public class AssemblyResolutionTests
{
    private readonly IServiceProvider _provider;
    public AssemblyResolutionTests()
    {
        var services = CreateDefaultServiceCollection();
        var configuration = GetDefaultConfiguration();
        services.AddSkillMediatorFromAssemblies(configuration, "AlexaSkillOptions",
            typeof(LaunchRequestHandler).Assembly);
        _provider = services.BuildServiceProvider();
    }

    [Fact]
    public void GetService_ShouldResolveMediator()
    {
        _provider.GetService<ISkillMediator>().Should().NotBeNull();
    }

    [Fact]
    public void GetService_ShouldResolveLaunchRequestHandler()
    {
        _provider.GetService<IRequestHandler<LaunchRequest>>().Should().NotBeNull();
    }

    [Fact]
    public void GetService_ShouldResolveSessionEndedRequestHandler()
    {
        _provider.GetService<IRequestHandler<SessionEndedRequest>>().Should().NotBeNull();
    }

    [Fact]
    public void GetService_ShouldResolveRequestInterceptor()
    {
        _provider.GetService<IRequestInterceptor>().Should().NotBeNull();
    }
    
    [Fact]
    public void GetService_ShouldResolveResponseInterceptor()
    {
        _provider.GetService<IResponseInterceptor>().Should().NotBeNull();
    }
    
    [Fact]
    public void GetService_ShouldResolveExceptionHandler()
    {
        _provider.GetService<IExceptionHandler>().Should().NotBeNull();
    }
    [Fact]
    public void GetService_ShouldResolveIntentRequestHandler()
    {
        _provider.GetService<IRequestHandler<IntentRequest>>().Should().NotBeNull();
    }

    private IServiceCollection CreateDefaultServiceCollection()
    {
        var services = new ServiceCollection();
        return services;
    }

    private IServiceProvider CreateDefaultServiceProvider(IServiceCollection serviceCollection)
    {
        return serviceCollection.BuildServiceProvider();
    }

    private static IConfiguration GetDefaultConfiguration()
    {
        var configuration = new Dictionary<string, string>
        {
            { "AlexaSkillOptions:SkillId", "0" }
        };
        return new ConfigurationBuilder().AddInMemoryCollection(configuration).Build();
    }

}