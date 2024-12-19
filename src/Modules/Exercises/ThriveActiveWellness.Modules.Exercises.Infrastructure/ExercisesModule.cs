using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ThriveActiveWellness.Common.Application.EventBus;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Infrastructure.Constants;
using ThriveActiveWellness.Common.Infrastructure.Outbox;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.MediaStorage;
using ThriveActiveWellness.Modules.Exercises.Domain.Clients;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Clients;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Database;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Equipment;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Exercises;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Inbox;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Outbox;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Storage;
using ThriveActiveWellness.Modules.Users.IntegrationEvents;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure;

public static class ExercisesModule
{
    public static WebApplicationBuilder AddExercisesModule(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddDomainEventHandlers();
        
        builder.Services.AddIntegrationEventHandlers();
        
        builder.AddInfrastructure(configuration);
        
        builder.Services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        
        return builder;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator, string instanceId)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>()
            .Endpoint(c => c.InstanceId = instanceId);
    }
    
    private static void AddInfrastructure(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.AddNpgsqlDbContext<ExercisesDbContext>(ServiceNames.Database, 
            configureDbContextOptions: options =>
            {
                options
                    .UseNpgsql(
                        configuration.GetConnectionString(ServiceNames.Database),
                        npgsqlOptions => npgsqlOptions
                            .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Exercises))
                    .UseSnakeCaseNamingConvention()
                    .AddInterceptors(new InsertOutboxMessagesInterceptor());
            });

        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
        builder.Services.AddScoped<IMuscleGroupRepository, MuscleGroupRepository>();
        
        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ExercisesDbContext>());
        
        builder.Services.Configure<OutboxOptions>(configuration.GetSection("Exercises:Outbox"));

        builder.Services.ConfigureOptions<ConfigureProcessOutboxJob>();

        builder.Services.Configure<InboxOptions>(configuration.GetSection("Exercises:Inbox"));

        builder.Services.ConfigureOptions<ConfigureProcessInboxJob>();
        
        builder.Services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration.GetSection("Storage"));
        });

        builder.Services.AddSingleton<IStorageService, AzureStorageService>();
    }
    
    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToArray();

        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);

            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }
    
    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        Type[] integrationEventHandlers = Presentation.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .ToArray();

        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);

            Type integrationEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler =
                typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }
}
