
using Bogus;
using MinimalApiPlayground.Global;
using MinimalApiPlayground.Models.Customer;
using MinimalApiPlayground.Models.DTO.Filters;
using MinimalApiPlayground.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimalApiPlayground;
public class SimpleCustomerDictionaryRepository : ICustomerRepository
{
    private readonly Dictionary<Guid, SimpleCustomer> _customers = new();

    #region Método Privado
    private bool FilterCustomer(SimpleCustomerFilter filter, SimpleCustomer customer)
    {
        return filter.Phone != customer.Phone ||
               filter.Name != customer.Name ||
               filter.LastName != customer.LastName ||
               filter.Email != customer.Email ||
               filter.Birthdate != customer.Birthdate.ToString();
    }
    #endregion

    #region Métodos Públicos
    public List<SimpleCustomer> Get(SimpleCustomerFilter filter)
    {
        var customers = _customers.Values.Where(customer =>
        {
            if (filter is not null &&
                (FilterCustomer(filter, customer)))
                return false;

            return true;
        });

        return customers.ToList();
    }

    public SimpleCustomer GetById(Guid id)
    {
        var customer = _customers.ToList().Find(customer => customer.Key == id).Value;

        return customer;
    }

    public bool Delete(Guid id)
    {
        return _customers.Remove(id);
    }

    public SimpleCustomer Update(SimpleCustomer customer)
    {
        Delete(customer.Id);

        _customers.Add(customer.Id, customer);

        return customer;
    }

    public Guid Create(SimpleCustomerFilter customer)
    {
        var id = Guid.NewGuid();

        _customers.Add(id, new SimpleCustomer()
        {
            Id = id,
            Name = customer.Name,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone
        });

        return id;
    }

    public List<SimpleCustomer> Randomize(int quantity)
    {
        var customers = Fakers.SimpleCustomer(quantity);

        if (customers is not null)
            customers.ForEach(customer =>
            {
                _customers.Add(customer.Id, customer);
            });
        else
            return new List<SimpleCustomer>();

        return customers;
    }
    #endregion
}