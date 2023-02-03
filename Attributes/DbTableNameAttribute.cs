namespace LibraryWebApi.Attributes;

public sealed class DbTableNameAttribute : System.Attribute
{
    public string TableName { get; set; }
}