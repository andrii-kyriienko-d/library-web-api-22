﻿namespace LibraryWebApi.Models.OptionsModels;

internal sealed class DatabaseOptions
{
    public string ConnectionString { get; set; }
    public string DBMSType { get; set; }
}