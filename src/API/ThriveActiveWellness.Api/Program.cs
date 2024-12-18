using System.Reflection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using ThriveActiveWellness.Api.Extensions;
using ThriveActiveWellness.Api.Middleware;
using ThriveActiveWellness.Api.OpenTelemetry;
using ThriveActiveWellness.Common.Application;
using ThriveActiveWellness.Common.Infrastructure;
using ThriveActiveWellness.Common.Infrastructure.Configuration;
using ThriveActiveWellness.Common.Infrastructure.Constants;
using ThriveActiveWellness.Common.Infrastructure.EventBus;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Modules.Exercises.Infrastructure;
using ThriveActiveWellness.Modules.Notifications.Infrastructure;
using ThriveActiveWellness.Modules.Users.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation(builder.Configuration);

Assembly[] moduleApplicationAssemblies = [
    ThriveActiveWellness.Modules.Exercises.Application.AssemblyReference.Assembly,
    ThriveActiveWellness.Modules.Notifications.Application.AssemblyReference.Assembly,
    ThriveActiveWellness.Modules.Users.Application.AssemblyReference.Assembly
];

builder.Services.AddApplication(moduleApplicationAssemblies);

string databaseConnectionString = builder.Configuration.GetConnectionStringOrThrow(ServiceNames.Database);
string redisConnectionString = builder.Configuration.GetConnectionStringOrThrow(ServiceNames.Redis);
var rabbitMqSettings = new RabbitMqSettings(builder.Configuration.GetConnectionStringOrThrow(ServiceNames.Queue));

builder.Services.AddInfrastructure(
    DiagnosticsConfig.ServiceName,
    [
        ExercisesModule.ConfigureConsumers,
        NotificationsModule.ConfigureConsumers
    ],
    rabbitMqSettings,
    databaseConnectionString,
    redisConnectionString,
    builder.Configuration);

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString)
    .AddRabbitMQ(rabbitConnectionString: rabbitMqSettings.Host);

builder.Configuration.AddModuleConfiguration(["users", "exercises", "notifications"]);

builder.Services.AddExercisesModule(builder.Configuration);

builder.Services.AddNotificationsModule(builder.Configuration);

builder.Services.AddUsersModule(builder.Configuration);

builder.Services.AddCors();

ConfigurationManager configuration = builder.Configuration;

WebApplication app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthAppName($"{configuration["Swagger:AppName"]}");
        c.OAuthClientId($"{configuration["Swagger:ClientId"]}");
        c.OAuthClientSecret($"{configuration["Swagger:ClientSecret"]}");
        c.ConfigObject.AdditionalItems.Add("tagsSorter", "alpha");
    });

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseLogContext();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

RouteGroupBuilder routeGroupBuilder = app.MapGroup("api");

app.MapEndpoints(routeGroupBuilder);

await app.RunAsync();

public partial class Program;

