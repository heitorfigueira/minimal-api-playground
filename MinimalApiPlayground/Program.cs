using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using MinimalApiPlayground.Framework;
using MinimalApiPlayground.Framework.Configuration;
using MinimalApiPlayground.Models.Customer;

#region Initialization
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointDefinitions(typeof(IEndpointDefinition));

services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SimpleCustomer>());

//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
//services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//        .RequireAuthenticatedUser()
//        .Build();
//});

services.UseSwagger();

#endregion

#region Configuration
var app = builder.Build();

app.ConfigureEndpointDefinitions();
app.ConfigureSwaggerEndpoints();

app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();


#endregion

app.Run();