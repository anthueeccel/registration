using FluentValidation;
using register.domain.Commands;
using System;

namespace register.domain.Validator.Customer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("Please, must inform the Id.");
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("Sorry, first name can't be empty.");
            RuleFor(c => c.BirthDate)
                .NotEqual(DateTime.MinValue).WithMessage("Sorry, the birth date can't be empty.")
                .Must(Validators.IsDateValid).WithMessage("Sorry, the birthdate informed is invalid.");
            RuleFor(c => c.Gender)
                .IsInEnum().WithMessage("Sorry, the Gender informed is invalid.").When(v => v.Gender != null);
        }
    }
}
