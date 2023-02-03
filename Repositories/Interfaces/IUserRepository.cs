using LibraryWebApi.Models.AuthModels;

namespace LibraryWebApi.Repositories.Interfaces;

internal interface IUserRepository
{
    UserDataToObjectModel GetUser(UserModel userModel);
}