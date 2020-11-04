using FluentValidation;
using register.domain.Commands;
using register.domain.Enum;
using System;

namespace register.domain.Validator
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("Sorry, first name can't be empty.");
            RuleFor(c => c.BirthDate)
                .NotEqual(DateTime.MinValue).WithMessage("Sorry, the birth date can't be empty.")
                .Must(Validators.IsDateValid).WithMessage("Sorry, the birthdate informed is invalid.");
            RuleFor(c => c.Gender)
                .NotEqual((GenderType)0).WithMessage("Must inform a valid gender.")
                .IsInEnum().WithMessage("Sorry, the Gender informed is invalid.").When(v => v.Gender != null);
        }
    }
}
