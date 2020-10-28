using FluentValidation.Results;
using register.domain.Enum;
using register.domain.Validator;
using register.domain.Validator.Customer;
using System;

namespace register.domain.Commands
{
    public class UpdateCustomerCommand : Command
    {
        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderType? Gender { get; set; }

        public UpdateCustomerCommand(Guid id, string firstName, string lastName, DateTime birthDate, GenderType gender)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
