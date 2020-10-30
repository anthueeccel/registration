using FluentValidation.Results;
using register.domain.Validator.Customer;
using System;

namespace register.domain.Commands
{
    public class RemoveCustomerCommand : Command
    {
        public Guid Id { get; set; }

        public RemoveCustomerCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCustomerCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
