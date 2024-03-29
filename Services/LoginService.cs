﻿using LibraryWebApi.Exceptions;
using LibraryWebApi.Models.AuthModels;
using LibraryWebApi.Models.OptionsModels;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Services.Interfaces;
using LibraryWebApi.Services.TokenService;
using Microsoft.Extensions.Options;

namespace LibraryWebApi.Services;

internal sealed class LoginService : ILoginService
{
    private readonly JwtOptions _jwtOptions;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public LoginService(IOptions<JwtOptions> jwtOptions, ITokenService tokenService, IUserRepository userRepository)
    {
        _jwtOptions = jwtOptions.Value;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }
    public string GenerateToken(UserModel userModel)
    {
        var generatedToken = "";
        var validUser = GetUser(userModel);

        if (validUser == null)
        {
            throw new BusinessException("User not valid");
        }
        generatedToken = _tokenService.BuildToken(_jwtOptions.Key, _jwtOptions.Issuer, validUser);

        if (generatedToken == null)
        {
            throw new BusinessException("Token is null");
        }
        return generatedToken;
    }

    private UserDataToObjectModel GetUser(UserModel userModel)
    {
        return _userRepository.GetUser(userModel);
    }
}