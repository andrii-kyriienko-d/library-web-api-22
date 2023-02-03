using LibraryWebApi.Models.AuthModels;

namespace LibraryWebApi.Services.TokenService;

internal interface ITokenService
{
    string BuildToken(string key, string issuer, UserDTO user);
}