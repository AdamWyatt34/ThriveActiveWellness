using Microsoft.OpenApi.Models;

namespace ThriveActiveWellness.Api.Extensions;

internal static class SwaggerExtensions
{
    internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c => 
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = configuration["Swagger:AppName"], Version = "v1" });
            c.EnableAnnotations();
    
            c.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
            
            string authUrl = $"https://{configuration["Swagger:CompanyName"]}.b2clogin.com/{configuration["Swagger:CompanyName"]}.onmicrosoft.com/{configuration["AzureAdB2C:SignUpSignInPolicyId"]}/oauth2/v2.0/authorize";
            string tokenUrl = $"https://{configuration["Swagger:CompanyName"]}.b2clogin.com/{configuration["Swagger:CompanyName"]}.onmicrosoft.com/{configuration["AzureAdB2C:SignUpSignInPolicyId"]}/oauth2/v2.0/token";
    
            // To Enable authorization using Swagger (JWT)
            var oauth = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Description = "Azure Active Directory B2C OAuth2 Bearer",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(authUrl),
                        TokenUrl = new Uri(tokenUrl),
                        Scopes = new Dictionary<string, string>
                        {
                            { $"{configuration["AzureAdB2C:Scopes"]}", "Api Access" }
                        }
                    }
                },
            };

            c.AddSecurityDefinition("oauth2", oauth);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}
