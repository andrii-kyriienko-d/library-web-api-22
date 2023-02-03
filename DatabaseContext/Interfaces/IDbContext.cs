using LibraryWebApi.Models.OptionsModels;
using SqlKata.Compilers;
using System.Data;

namespace LibraryWebApi.DatabaseContextFactory.Interfaces;

public interface IDbContext
{
    public IDbConnection GetConnection(DatabaseOptions dbOptions);
    public Compiler GetCompiler();
}