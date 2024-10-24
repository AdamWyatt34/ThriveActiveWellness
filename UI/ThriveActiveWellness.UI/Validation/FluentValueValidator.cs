using FluentValidation;
using FluentValidation.Results;

namespace ThriveActiveWellness.UI.Validation;

/// <summary>
/// A glue class to make it easy to define validation rules for single values using FluentValidation
/// You can reuse this class for all your fields, like for the credit card rules above.
/// </summary>
/// <typeparam name="T"></typeparam>
public class FluentValueValidator<T> : AbstractValidator<T>
{
    public FluentValueValidator(Action<IRuleBuilderInitial<T, T>> rule)
    {
        rule(RuleFor(x => x));
    }

    private IEnumerable<string> ValidateValue(T arg)
    {
        ValidationResult result = Validate(arg);
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    }

    public Func<T, IEnumerable<string>> Validation => ValidateValue;
}
