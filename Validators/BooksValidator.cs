using FluentValidation;
using LibraryWebApi.Entities;

namespace LibraryWebApi.Validators;

internal class BooksValidator : AbstractValidator<Books>
{
    public BooksValidator()
    {
        RuleFor(item => item.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(1);

        RuleFor(item => item.Pages)
            .GreaterThan(0)
            .NotEmpty()
            .NotNull();
    }
}