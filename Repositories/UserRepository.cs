using System;
using LibraryWebApi.Models.AuthModels;
using LibraryWebApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWebApi.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private List<UserDataToObjectModel> Users { get; } = new();

    public UserRepository()
    {
        Users.Add(new UserDataToObjectModel
        {
            UserName = "admin",
            Password = "admin",
            Role = "admin"
        });
    }
    public UserDataToObjectModel GetUser(UserModel userModel)
    {
        return Users.FirstOrDefault(x => string.Equals(x.UserName, userModel.UserName, StringComparison.CurrentCultureIgnoreCase)
                                          && x.Password == userModel.Password);
    }
}