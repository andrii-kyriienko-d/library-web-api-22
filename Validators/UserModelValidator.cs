using FluentValidation;
using LibraryWebApi.Models.AuthModels;

namespace LibraryWebApi.Validators;

internal class UserModelValidator : AbstractValidator<UserModel>
{
    public UserModelValidator()
    {
        RuleFor(item => item.Password)
            .NotEmpty()
            .NotNull();

        RuleFor(item => item.UserName)
            .NotEmpty()
            .NotNull();
    }
}