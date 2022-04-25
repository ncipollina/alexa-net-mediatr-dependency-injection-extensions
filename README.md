# Alexa.Net.MediatR extensions for Microsoft.Extensions.DependencyInjection

## Package Version

| Build Status                                                                                                                                                                                    | Nuget                                                                                                                                                                                                  |
|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [![.Net Build and Package](https://github.com/ncipollina/alexa-net-mediatr-dependency-injection-extensions/actions/workflows/build.yaml/badge.svg)](https://github.com/ncipollina/alexa-net-mediatr-dependency-injection-extensions/actions/workflows/build.yaml) | [![NuGet Badge](https://buildstats.info/nuget/alexa.net.mediatr.extensions.microsoft.dependencyinjection)](https://www.nuget.org/packages/Alexa.Net.MediatR.Extensions.Microsoft.DependencyInjection/) |

## Installation

Install the package via nuget:

```powershell

Install-Package Alexa.Net.MediatR.Extensions.Microsoft.DependencyInjection

```

Or via the .Net command line interface:

```bash

dotnet add package Alexa.Net.MediatR.Extensions.Microsoft.DependencyInjection

```

## Use

This package scans assemblies and adds handlers, request and response interceptors, and exception handlers to the containers. It also registers the necessary `AlexaSkillOptions` instance to verify/validate the skill. This will also scan the registered assemblies for an instance of the `IDefaultHandler` interface and will throw an exception if more than one instance is found. To add with a type:

```c#

services.AddSkillMediatorFromAssemblies(configuration, "AlexaSkillOptions", typeof(MyCustomHandler));

```

or with an assembly:

```c#

services.AddSkillMediatorFromAssemblies(configuration, "AlexaSkillOptions", typeof(Function).GetTypeInfo().Assembly);

```
