using LibraryWebApi.Models.AuthModels;

namespace LibraryWebApi.Repositories.Interfaces;

public interface IUserRepository
{
    public UserDTO GetUser(UserModel userModel);
}