using LibraryWebApi.Models.AuthModels;
using LibraryWebApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWebApi.Repositories;

public sealed class UserRepository : IUserRepository
{
    private List<UserDTO> _users { get; set; } = new List<UserDTO>();

    public UserRepository()
    {
        _users.Add(new UserDTO
        {
            UserName = "admin",
            Password = "admin",
            Role = "admin"
        });
    }
    public UserDTO GetUser(UserModel userModel)
    {
        return _users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower()
                                 && x.Password == userModel.Password).FirstOrDefault();
    }
}