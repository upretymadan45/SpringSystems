namespace Spring.Application.Validations;
public sealed class EmployeeValidator : AbstractValidator<EmployeeDTO>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name cannot be empty")
            .MinimumLength(2).WithMessage("First Name must be at least 2 characters in length");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name cannot be empty")
            .MinimumLength(2).WithMessage("Last Name must be at least 2 characters in lenght");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address cannot be empty")
            .MinimumLength(2).WithMessage("Address must be at least 2 characters in lenght");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Invalid Email");

        RuleFor(x => x.ContactNo)
            .NotEmpty().WithMessage("Contact number cannot be empty")
            .MatchPhoneNumberRule().WithMessage("Invalid Contact Number");

        RuleFor(x => x.CompanyId)
            .GreaterThanOrEqualTo(1).WithMessage("Invalid Company");
    }
}
