using TheiveActiveWellness.MigrationService;
using ThriveActiveWellness.Common.Infrastructure.Constants;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Database;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Database;
using ThriveActiveWellness.Modules.Users.Infrastructure.Database;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<ExercisesDbContext>(ServiceNames.Database);
builder.AddNpgsqlDbContext<NotificationsDbContext>(ServiceNames.Database);
builder.AddNpgsqlDbContext<UsersDbContext>(ServiceNames.Database);

IHost host = builder.Build();
await host.RunAsync();
