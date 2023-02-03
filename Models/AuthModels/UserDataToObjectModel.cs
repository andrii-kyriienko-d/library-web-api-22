namespace LibraryWebApi.Models.AuthModels;

internal sealed class UserDataToObjectModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}