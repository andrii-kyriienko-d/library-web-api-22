using FluentValidation;
using LibraryWebApi.Entities;

namespace LibraryWebApi.Validators;

internal sealed class PublishersValidator : AbstractValidator<Publishers>
{
    public PublishersValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();

        RuleFor(item => item.Location)
            .NotEmpty();

        RuleFor(item => item.CompanyUCode)
            .InclusiveBetween(100000, 9999999);

    }
}