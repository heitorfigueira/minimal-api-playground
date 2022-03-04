
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimalApiPlayground.Framework.Configuration;
public static class EndpointDefinitionExtensions
{
    public static void AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
    {
        var endpointDefinitons = new List<IEndpointDefinition>();

        foreach (var marker in scanMarkers)
        {
            endpointDefinitons.AddRange(marker.Assembly.ExportedTypes
                .Where(typeX => typeof(IEndpointDefinition).IsAssignableFrom(typeX) && !typeX.IsInterface)
                .Select(Activator.CreateInstance).Cast<IEndpointDefinition>());

            foreach (var endpoint in endpointDefinitons)
            {
                endpoint.DefineServices(services);
            }

            services.AddSingleton(endpointDefinitons as IReadOnlyCollection<IEndpointDefinition>);
        }
    }

    public static void ConfigureEndpointDefinitions(this WebApplication app)
    {
        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

        foreach (var endpoint in definitions)
        {
            endpoint.DefineEndpoints(app);
            endpoint.SatisfyDependencies(app);
        }
    }

}
