using LibraryWebApi.Models.AuthModels;
using LibraryWebApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWebApi.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private List<UserDataToObjectModel> _users { get; set; } = new List<UserDataToObjectModel>();

    public UserRepository()
    {
        _users.Add(new UserDataToObjectModel
        {
            UserName = "admin",
            Password = "admin",
            Role = "admin"
        });
    }
    public UserDataToObjectModel GetUser(UserModel userModel)
    {
        return _users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower()
                                 && x.Password == userModel.Password).FirstOrDefault();
    }
}