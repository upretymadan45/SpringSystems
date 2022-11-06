using FluentValidation.Validators;

namespace Spring.Application.Validations;
public static class CustomValidators
{
    public static IRuleBuilderOptions<T, string> MatchPhoneNumberRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new RegularExpressionValidator<T>(@"(^(?:[0-9]\-?){5,13}[0-9]$)|(^(?:[0-9]\x20?){5,13}[0-9]$)"));
    }
}
