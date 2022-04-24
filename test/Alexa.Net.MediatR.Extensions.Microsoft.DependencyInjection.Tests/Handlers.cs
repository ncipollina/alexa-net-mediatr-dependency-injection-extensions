using System;
using System.Threading;
using System.Threading.Tasks;
using Alexa.Net.MediatR.Pipeline;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace Alexa.Net.MediatR.Extensions.Microsoft.DependencyInjection.Tests;

public class LaunchRequestHandler : IRequestHandler<LaunchRequest>
{
    public Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken = new CancellationToken())
    {
        return input.ResponseBuilder.Empty(cancellationToken);
    }
}

public class SessionEndHandler : IRequestHandler<SessionEndedRequest>
{
    public Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken = new CancellationToken())
    {
        return input.ResponseBuilder.Empty(cancellationToken);
    }
}

public class IntentHandler : IRequestHandler<IntentRequest>
{
    public Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken = new CancellationToken())
    {
        return input.ResponseBuilder.Empty(cancellationToken);
    }
}

public class ResponseInterceptor : IResponseInterceptor
{
    public Task Process(IHandlerInput input, SkillResponse output, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.CompletedTask;
    }
}

public class RequestInterceptor : IRequestInterceptor
{
    public Task Process(IHandlerInput input, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.CompletedTask;
    }
}

public class ExceptionHandler : IExceptionHandler
{
    public Task<bool> CanHandle(IHandlerInput handlerInput, Exception ex, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(true);
    }

    public Task<SkillResponse> Handle(IHandlerInput handlerInput, Exception ex, CancellationToken cancellationToken = new CancellationToken())
    {
        return handlerInput.ResponseBuilder.Empty(cancellationToken);
    }
}