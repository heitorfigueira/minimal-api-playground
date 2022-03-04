using Bogus;
using MinimalApiPlayground.Models.Customer;
using System;
using System.Collections.Generic;

namespace MinimalApiPlayground.Global
{
    public class Fakers
    {
        public static List<SimpleCustomer> SimpleCustomer(int quantity)
        {
            var faker = new Faker<SimpleCustomer>("pt_BR")
                                .RuleFor(prop => prop.Id, fak => Guid.NewGuid())
                                .RuleFor(prop => prop.Name, fak => fak.Person.FirstName)
                                .RuleFor(prop => prop.LastName, fak => fak.Person.LastName)
                                .RuleFor(prop => prop.Phone, fak => fak.Person.Phone)
                                .RuleFor(prop => prop.Email, fak => fak.Person.Email)
                                .RuleFor(prop => prop.Birthdate, fak => fak.Person.DateOfBirth.Date);

            return faker.Generate(quantity);
        }
    }
}
