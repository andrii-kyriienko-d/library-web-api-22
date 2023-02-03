using LibraryWebApi.Models.OptionsModels;
using LibraryWebApi.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;

namespace LibraryWebApi.Services;

internal sealed class LastLogGetterService : ILastLogGetter
{
    private readonly FileLoggerOptions _fileLoggerOptions;
    private readonly IMemoryCache _cache;
    public LastLogGetterService(IOptions<FileLoggerOptions> fileLoggerOptions, IMemoryCache сache)
    {
        _fileLoggerOptions = fileLoggerOptions.Value;
        _cache = сache;
    }

    public string GetLastLog()
    {
        var lastLogData = "";


        var file =
            new DirectoryInfo(_fileLoggerOptions.FolderPath)
                .GetFiles()
                .OrderBy(f => f.CreationTime)
                .Last();

        var fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        using (var sr = new StreamReader(fs))
        {
            lastLogData += sr.ReadToEnd();
        }

        return lastLogData;
    }
}