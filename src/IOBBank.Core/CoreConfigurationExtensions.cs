﻿using System.Reflection;
using IOBBank.Core.Mediator.Pipelines;
using IOBBank.Core.Notifications;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IOBBank.Core;

public static class CoreConfigurationExtensions
{
    public static IServiceCollection AddCqrs(
        this IServiceCollection services,
        Assembly assembly,
        Action<CoreConfiguration>? config = null)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return services.AddCqrs(new[] { assembly }, config);
    }

    public static IServiceCollection AddCqrs(
        this IServiceCollection services,
        Assembly[] assemblies,
        Action<CoreConfiguration>? config = null)
    {
        if (assemblies is not { Length: > 0 })
        {
            throw new ArgumentNullException(nameof(assemblies));
        }

        var cqrsConfiguration = new CoreConfiguration(assemblies);

        config?.Invoke(cqrsConfiguration);

        return services
            .AddMediator(cqrsConfiguration)
            .AddValidators(cqrsConfiguration)
            .AddPipelines(cqrsConfiguration);
    }

    private static IServiceCollection AddMediator(this IServiceCollection services,
        CoreConfiguration coreConfiguration)
    {
        return services.AddMediatR(config =>
        {
            config.AsScoped();
        }, coreConfiguration.Assemblies);
    }

    private static IServiceCollection AddValidators(this IServiceCollection services,
        CoreConfiguration cqrsConfiguration)
    {
        services.AddValidatorsFromAssemblies(cqrsConfiguration.Assemblies);

        return services;
    }

    private static IServiceCollection AddPipelines(this IServiceCollection services,
        CoreConfiguration coreConfiguration)
    {
        return services
            .AddScoped<INotifier, Notifier>()
            .AddExceptionPipeline(coreConfiguration)
            .AddValidationPipeline(coreConfiguration)
            .AddNotificationPipeline(coreConfiguration);
    }

    private static IServiceCollection AddExceptionPipeline(this IServiceCollection services,
        CoreConfiguration coreConfiguration)
    {
        if (!coreConfiguration.WithExceptionPipeline) return services;

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));

        return services;
    } 

    private static IServiceCollection AddValidationPipeline(this IServiceCollection services,
        CoreConfiguration coreConfiguration)
    {
        if (!coreConfiguration.WithValidationPipeline) return services;

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return services;
    }

    private static IServiceCollection AddNotificationPipeline(this IServiceCollection services,
        CoreConfiguration coreConfiguration)
    {
        if (!coreConfiguration.WithNotificationPipeline) return services;

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(NotificationPipelineBehavior<,>));

        return services;
    }
}