
using FluentValidation;
using MinimalApiPlayground.Models;
using MinimalApiPlayground.Models.Customer;

namespace MinimalApiPlayground.Validations;
public class CustomerValidator : AbstractValidator<SimpleCustomer>
{
    public CustomerValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
    }
}
