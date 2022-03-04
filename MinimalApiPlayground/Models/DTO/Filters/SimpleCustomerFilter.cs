using System;

namespace MinimalApiPlayground.Models.DTO.Filters
{
    public class SimpleCustomerFilter 
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Birthdate { get; set; }
    }
}
