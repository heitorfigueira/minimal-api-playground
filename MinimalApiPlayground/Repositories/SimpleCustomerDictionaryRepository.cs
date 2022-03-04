
using Bogus;
using MinimalApiPlayground.Models.Customer;
using MinimalApiPlayground.Models.DTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimalApiPlayground.Repositories;
public class SimpleCustomerDictionaryRepository : ICustomerRepository
{
    private readonly Dictionary<Guid, SimpleCustomer> _customers = new();

    private List<SimpleCustomer> GenerateRandomSimpleCustomer(int quantity)
    {
        var customerFaker = new Faker<SimpleCustomer>()
                                .RuleFor(prop => prop.Id, fak => Guid.NewGuid())
                                .RuleFor(prop => prop.Name, fak => fak.Person.FirstName)
                                .RuleFor(prop => prop.LastName, fak => fak.Person.LastName)
                                .RuleFor(prop => prop.Phone, fak => fak.Person.Phone)
                                .RuleFor(prop => prop.Email, fak => fak.Person.Email);

        return customerFaker.Generate(quantity).ToList();
    }

    public List<SimpleCustomer> Get(SimpleCustomerFilter filter)
    {
        var customers = _customers.Values.Where(customer =>
        {
            if (filter is not null &&
                (filter.Phone != customer.Phone ||
                filter.Name != customer.Name ||
                filter.LastName != customer.LastName ||
                filter.Email != customer.Email))
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

    public List<SimpleCustomer> Randomize(int number)
    {
        var customers = GenerateRandomSimpleCustomer(number);

        if (customers is not null)
            customers.ForEach(customer =>
            {
                _customers.Add(customer.Id, customer);
            });
        else
            return new List<SimpleCustomer>();

        return customers;
    }
}
