using FluentValidation;
using register.domain.Commands;

namespace register.domain.Validator
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("Sorry, first name can't be empty.");
            RuleFor(c => c.BirthDate)
                .NotEmpty().WithMessage("Sorry, the birthdate can't be empty.")
                .Must(Validators.IsDateValid).WithMessage("Sorry, the birthdate informed is invalid.");
            RuleFor(c => c.Gender)
                .IsInEnum().WithMessage("Sorry, the Gender informed is invalid.").When(v => v.Gender != null);
        }
    }
}
