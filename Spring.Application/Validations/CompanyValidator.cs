namespace Spring.Application.Validations;
public sealed class CompanyValidator : AbstractValidator<CompanyDTO>
{
    public CompanyValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Company Name cannot be empty")
            .MinimumLength(2).WithMessage("Company Name must be atleast 2 characters in lenght");

        RuleFor(c => c.Address)
            .NotEmpty().WithMessage("Company Address cannot be empty")
            .MinimumLength(2).WithMessage("Company Address must be at least 2 characters in length");
    }
}
