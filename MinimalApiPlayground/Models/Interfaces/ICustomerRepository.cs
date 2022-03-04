using MinimalApiPlayground.Models.Customer;
using MinimalApiPlayground.Models.DTO.Filters;
using System;
using System.Collections.Generic;

namespace MinimalApiPlayground.Models.Interfaces
{
    public interface ICustomerRepository
    {
        List<SimpleCustomer> Get(SimpleCustomerFilter filter);
        SimpleCustomer GetById(Guid id);
        Guid Create(SimpleCustomerFilter customer);
        List<SimpleCustomer> Randomize(int number);
        bool Delete(Guid id);
        SimpleCustomer Update(SimpleCustomer customer);
    }
}