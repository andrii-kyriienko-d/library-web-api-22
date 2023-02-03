namespace LibraryWebApi.Models.OptionsModels;

internal sealed class JwtOptions
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}