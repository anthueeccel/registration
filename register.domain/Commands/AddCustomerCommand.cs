using FluentValidation.Results;
using register.domain.Enum;
using register.domain.Validator;
using System;

namespace register.domain.Commands
{
    public class AddCustomerCommand : Command
    {
        protected Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderType? Gender { get; set; }

        public AddCustomerCommand(string firstName, string lastName, DateTime birthDate, GenderType gender)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddCustomerCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
