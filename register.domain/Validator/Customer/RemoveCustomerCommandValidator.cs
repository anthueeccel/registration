using FluentValidation;
using register.domain.Commands;
using System;

namespace register.domain.Validator.Customer
{
    public class RemoveCustomerCommandValidator : AbstractValidator<RemoveCustomerCommand>
    {
        public RemoveCustomerCommandValidator()
        {
            RuleFor(c => c.Id)
               .NotEqual(Guid.Empty).WithMessage("Please, must inform the Id.");
        }
    }
}
