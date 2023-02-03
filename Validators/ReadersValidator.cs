using FluentValidation;
using LibraryWebApi.Entities;

namespace LibraryWebApi.Validators;

internal sealed class ReadersValidator : AbstractValidator<Readers>
{
    public ReadersValidator()
    {
        RuleFor(item => item.LibraryCode)
            .Length(5, 7);

        RuleFor(item => item.Age)
            .GreaterThan(0);

        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();

    }
}