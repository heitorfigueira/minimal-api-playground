
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MinimalApiPlayground.Framework;
public interface IEndpointDefinition
{
    void DefineServices(IServiceCollection services);
    void DefineEndpoints(WebApplication app);
    void SatisfyDependencies(WebApplication app);
}
