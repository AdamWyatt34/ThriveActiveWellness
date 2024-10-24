using BoldSign.Api;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SendGrid.Extensions.DependencyInjection;
using ThriveActiveWellness.Common.Application.EventBus;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Infrastructure.Configuration;
using ThriveActiveWellness.Common.Infrastructure.Outbox;
using ThriveActiveWellness.Constants;
using ThriveActiveWellness.Modules.Notifications.Application.Abstractions;
using ThriveActiveWellness.Modules.Notifications.Domain.Notifications;
using ThriveActiveWellness.Modules.Notifications.Domain.Users;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Database;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.DocumentSigning;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Emails;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Inbox;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Notifications;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Outbox;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Pdfs;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Users;
using ThriveActiveWellness.Modules.Users.IntegrationEvents;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure;

public static class NotificationsModule
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomainEventHandlers();
        
        services.AddIntegrationEventHandlers();
        
        services.AddInfrastructure(configuration);
        
        return services;
    }
    
    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator, string instanceId)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>()
            .Endpoint(c => c.InstanceId = instanceId);
    }
    
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NotificationsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString(ServiceNames.Database),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Notifications))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        
        services.AddTransient<IEmailService, EmailService>();
        services.AddSendGrid(options =>
        {
            string sendGridApiKey = configuration.GetValueOrThrow<string>("SendGrid:ApiKey");
            options.ApiKey = sendGridApiKey;
        });
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<NotificationsDbContext>());
        
        services.Configure<OutboxOptions>(configuration.GetSection("Notifications:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<InboxOptions>(configuration.GetSection("Notifications:Inbox"));

        services.ConfigureOptions<ConfigureProcessInboxJob>();

        services.AddSingleton<IPdfGenerator, PdfGenerator>();

        services.AddSingleton<ITokenService, BoldSignTokenService>();
        services.AddSingleton<IDocumentService, BoldSignDocumentService>();
        services.AddSingleton<ApiClient>();
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
