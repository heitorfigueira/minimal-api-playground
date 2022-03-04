
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiPlayground.Framework;
using MinimalApiPlayground.Framework.Models;
using MinimalApiPlayground.Models.Customer;
using MinimalApiPlayground.Models.DTO.Filters;
using MinimalApiPlayground.Models.Interfaces;
using MinimalApiPlayground.Repositories;
using System;
using System.Collections.Generic;

namespace MinimalApiPlayground.EndpointDefinitions;
public class CustomerDefinitions : IEndpointDefinition
{
    private ICustomerRepository _customerRepo;

    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/customer/create", Create);
        app.MapDelete("/customer", Delete);
        app.MapPut("/customer", Update);
        app.MapGet("/customer/{id}", GetById);

        app.MapPost("/customer", List);
        app.MapGet("/customer/randomize/{quantity}", Randomize);
    }

    #region Métodos Internos
    internal Response<SimpleCustomer> Update([FromBody] SimpleCustomer customer)
    {
        var newCustomer = _customerRepo.Update(customer);

        if (newCustomer is null)
            return Response.Empty<SimpleCustomer>();
        else
            return Response.Ok(newCustomer);
    }

    internal Response Delete(Guid id)
    {
        if (_customerRepo.Delete(id))
            return Response.Ok();
        else
            return Response.Ok("A requisição obteve sucesso porém não foi deletado nenhum registro.");
    }

    internal Response<List<SimpleCustomer>> List([FromBody] SimpleCustomerFilter filter)
    {
        var customers = _customerRepo.Get(filter);

        if (customers is null)
            return Response.EmptyList<SimpleCustomer>();

        return Response.Ok(customers);
    }

    internal Response<SimpleCustomer> GetById(Guid id)
    {
        var customer = _customerRepo.GetById(id);

        if (customer is null)
            return Response.Empty<SimpleCustomer>();

        return Response.Ok(customer);
    }

    internal Response<List<SimpleCustomer>> Randomize(int quantity)
    {
        var customers = _customerRepo.Randomize(quantity);

        if (customers is null)
            return Response.EmptyList<SimpleCustomer>();

        return Response.Ok(customers);
    }

    internal Response<Guid> Create([FromBody] SimpleCustomerFilter filter)
    {
        var id = _customerRepo.Create(filter);

        return Response.Ok(id);
    }
    #endregion

    #region DependencyConfiguration

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, SimpleCustomerDictionaryRepository>();

    }

    public void SatisfyDependencies(WebApplication app)
    {
        _customerRepo = app.Services.GetRequiredService<ICustomerRepository>();
    }
    #endregion
}
