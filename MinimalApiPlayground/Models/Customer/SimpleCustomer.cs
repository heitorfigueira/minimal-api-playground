using System;

namespace MinimalApiPlayground.Models.Customer
{
    public class SimpleCustomer
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
