namespace LibraryWebApi.Models.OptionsModels;

public sealed class FileLoggerOptions
{
    public string FolderPath { get; set; }
    public string Append { get; set; }
    public string MinLevel { get; set; }
}