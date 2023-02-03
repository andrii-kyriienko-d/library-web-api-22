using LibraryWebApi.Models.AuthModels;

namespace LibraryWebApi.Services.Interfaces;

public interface ILoginService
{
    string GenerateToken(UserModel userModel);
}