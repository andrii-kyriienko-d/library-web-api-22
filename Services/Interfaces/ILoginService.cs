using LibraryWebApi.Models.AuthModels;

namespace LibraryWebApi.Services.Interfaces;

public interface ILoginService
{
    public string GenerateToken(UserModel userModel);
}