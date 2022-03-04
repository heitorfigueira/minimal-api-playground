
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace MinimalApiPlayground.Framework.Configuration;
public static class SwaggerExtension
{
    public static void UseSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Minimal Api Playground", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                { new OpenApiSecurityScheme 
                    { Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, new List<string> {}
                }
            });
        });
    }

    public static void ConfigureSwaggerEndpoints(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options => {
            options.SwaggerEndpoint($"/swagger/v1/swagger.json", "MinimalApiPlayground");
        });
    }

}
