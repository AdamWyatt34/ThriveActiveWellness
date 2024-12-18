using Refit;
using ThriveActiveWellness.UI.Enums;
using ThriveActiveWellness.UI.Options;
using ThriveActiveWellness.UI.Services.Clients;

namespace ThriveActiveWellness.UI.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        
        DownstreamApiConfiguration downstreamApiConfiguration = configuration.GetSection("DownstreamApi")
            .Get<DownstreamApiConfiguration>()!;
        
        services.AddScoped<CustomAuthorizationMessageHandler>();
        
        services.AddHttpClient(ClientConfiguration.WebApi.ToString(),
                client => client.BaseAddress = new Uri(downstreamApiConfiguration.BaseUrl!))
            .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

        services.AddHttpClient(ClientConfiguration.Unauthenticated.ToString(),
            client => client.BaseAddress = new Uri(downstreamApiConfiguration.BaseUrl!));
        
        return services;
    }
    
    public static IServiceCollection AddApiClients(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        
        DownstreamApiConfiguration downstreamApiConfiguration = configuration.GetSection("DownstreamApi")
            .Get<DownstreamApiConfiguration>()!;
        
        services.AddRefitClient<IEquipmentApi>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri($"{downstreamApiConfiguration.BaseUrl!}equipment"))
            .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
        
        services.AddRefitClient<IUserProfileApi>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri($"{downstreamApiConfiguration.BaseUrl!}users"))
            .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
        
        return services;
    }
}
