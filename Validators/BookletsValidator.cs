using FluentValidation;
using LibraryWebApi.Entities;

namespace LibraryWebApi.Validators;

internal sealed class BookletsValidator : AbstractValidator<Booklets>
{
    public BookletsValidator()
    {
        RuleFor(item => item.BookCount)
            .GreaterThan(0)
            .NotNull()
            .NotEmpty();

        RuleFor(item => item.FullName)
            .MinimumLength(1)
            .NotEmpty()
            .NotNull();

        RuleFor(item => item.Price)
            .GreaterThan(0);
    }
}