using Dapper;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Quartz;
using ThriveActiveWellness.Common.Application.Caching;
using ThriveActiveWellness.Common.Application.Clock;
using ThriveActiveWellness.Common.Application.Data;
using ThriveActiveWellness.Common.Application.EventBus;
using ThriveActiveWellness.Common.Infrastructure.Authentication;
using ThriveActiveWellness.Common.Infrastructure.Authorization;
using ThriveActiveWellness.Common.Infrastructure.Caching;
using ThriveActiveWellness.Common.Infrastructure.Clock;
using ThriveActiveWellness.Common.Infrastructure.Configuration;
using ThriveActiveWellness.Common.Infrastructure.Constants;
using ThriveActiveWellness.Common.Infrastructure.Data;
using ThriveActiveWellness.Common.Infrastructure.Outbox;

namespace ThriveActiveWellness.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static WebApplicationBuilder AddInfrastructure(
        this WebApplicationBuilder builder,
        string serviceName,
        Action<IRegistrationConfigurator, string>[] moduleConfigureConsumers,
        IConfiguration configuration)
    {
        builder.Services.AddAuthenticationInternal(configuration);
        
        builder.Services.AddAuthorizationInternal();

        builder.Services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        builder.Services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        builder.Services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        string databaseConnectionString = configuration.GetConnectionStringOrThrow(ServiceNames.Database);
        
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        builder.Services.TryAddSingleton(npgsqlDataSource);

        builder.Services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();

        SqlMapper.AddTypeHandler(new GenericArrayHandler<string>());

        builder.Services.AddQuartz(configurator =>
        {
            var scheduler = Guid.NewGuid();
            configurator.SchedulerId = $"default-id-{scheduler}";
            configurator.SchedulerName = $"default-name-{scheduler}";
        });

        builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        builder.AddRabbitMQClient(ServiceNames.Queue);
        #pragma warning disable EXTEXP0018
        builder.Services.AddHybridCache(o =>
        {
            o.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(5),
                LocalCacheExpiration = TimeSpan.FromMinutes(5)
            };
        });
        #pragma warning restore EXTEXP0018
        builder.AddRedisDistributedCache(ServiceNames.Redis);

        builder.Services.TryAddSingleton<ICacheService, CacheService>();

        builder.Services.AddMassTransit(configure =>
        {
            string instanceId = serviceName.ToLowerInvariant().Replace('.', '-');
            foreach (Action<IRegistrationConfigurator, string> configureConsumers in moduleConfigureConsumers)
            {
                configureConsumers(configure, instanceId);
            }

            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionStringOrThrow(ServiceNames.Queue));
                
                cfg.ConfigureEndpoints(context);
            });
        });

        return builder;
    }
}
