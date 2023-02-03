using FluentValidation;
using LibraryWebApi.Models.RequestModels;

namespace LibraryWebApi.Validators;

internal sealed class SearchModelValidator : AbstractValidator<SearchModel>
{
    public SearchModelValidator()
    {
        RuleFor(item => item.SearchExpression)
            .NotEmpty()
            .NotNull();
    }
}