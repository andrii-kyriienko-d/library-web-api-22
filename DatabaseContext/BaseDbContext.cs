﻿using LibraryWebApi.DatabaseContextFactory.Interfaces;
using LibraryWebApi.Models.OptionsModels;
using SqlKata.Compilers;
using System.Data;

namespace LibraryWebApi.DatabaseContextFactory;

internal abstract class BaseDbContext : IDbContext
{
    public abstract Compiler GetCompiler();
    public abstract IDbConnection GetConnection(DatabaseOptions dbOptions);
}