using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ThriveActiveWellness.Common.Application.Authorization;
using ThriveActiveWellness.Common.Application.EventBus;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Infrastructure.Constants;
using ThriveActiveWellness.Common.Infrastructure.Outbox;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Modules.Users.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Users.Domain.PARQ;
using ThriveActiveWellness.Modules.Users.Domain.Users;
using ThriveActiveWellness.Modules.Users.Infrastructure.Authorization;
using ThriveActiveWellness.Modules.Users.Infrastructure.Database;
using ThriveActiveWellness.Modules.Users.Infrastructure.Inbox;
using ThriveActiveWellness.Modules.Users.Infrastructure.Outbox;
using ThriveActiveWellness.Modules.Users.Infrastructure.PARQ;
using ThriveActiveWellness.Modules.Users.Infrastructure.Users;

namespace ThriveActiveWellness.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static WebApplicationBuilder AddUsersModule(
        this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddDomainEventHandlers();

        builder.Services.AddIntegrationEventHandlers();

        builder.AddInfrastructure(configuration);

        builder.Services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return builder;
    }

    private static void AddInfrastructure(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddScoped<IPermissionService, PermissionService>();

        builder.AddNpgsqlDbContext<UsersDbContext>(ServiceNames.Database, 
            configureDbContextOptions: options =>
            {
                options
                    .UseNpgsql(
                        configuration.GetConnectionString(ServiceNames.Database),
                        npgsqlOptions => npgsqlOptions
                            .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
                    .UseSnakeCaseNamingConvention()
                    .AddInterceptors(new InsertOutboxMessagesInterceptor());
            });

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IParQResponseRepository, ParQResponseRepository>();
        builder.Services.AddScoped<IParQCompletionRepository, ParQCompletionRepository>();

        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        builder.Services.Configure<OutboxOptions>(configuration.GetSection("Users:Outbox"));

        builder.Services.ConfigureOptions<ConfigureProcessOutboxJob>();

        builder.Services.Configure<InboxOptions>(configuration.GetSection("Users:Inbox"));

        builder.Services.ConfigureOptions<ConfigureProcessInboxJob>();
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
