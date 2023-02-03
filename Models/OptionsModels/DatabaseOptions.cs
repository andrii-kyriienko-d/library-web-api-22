namespace LibraryWebApi.Models.OptionsModels;

public sealed class DatabaseOptions
{
    public string ConnectionString { get; set; }
    public string DBMSType { get; set; }
}