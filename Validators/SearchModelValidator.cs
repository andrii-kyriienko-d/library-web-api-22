using FluentValidation;
using LibraryWebApi.Models.RequestModels;

namespace LibraryWebApi.Validators;

internal class SearchModelValidator : AbstractValidator<SearchModel>
{
    public SearchModelValidator()
    {
        RuleFor(item => item.SearchExpression)
            .NotEmpty()
            .NotNull();
    }
}