using System.Reflection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using ThriveActiveWellness.Api.Extensions;
using ThriveActiveWellness.Api.Middleware;
using ThriveActiveWellness.Api.OpenTelemetry;
using ThriveActiveWellness.Common.Application;
using ThriveActiveWellness.Common.Infrastructure;
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

builder.AddInfrastructure(
    DiagnosticsConfig.ServiceName,
    [
        ExercisesModule.ConfigureConsumers,
        NotificationsModule.ConfigureConsumers
    ],
    builder.Configuration);

builder.Configuration.AddModuleConfiguration(["users", "exercises", "notifications"]);

builder.AddExercisesModule(builder.Configuration);

builder.AddNotificationsModule(builder.Configuration);

builder.AddUsersModule(builder.Configuration);

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

