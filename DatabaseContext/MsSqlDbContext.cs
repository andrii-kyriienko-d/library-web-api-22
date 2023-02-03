using LibraryWebApi.Attributes;
using LibraryWebApi.Models.OptionsModels;
using SqlKata.Compilers;
using System.Data;
using System.Data.SqlClient;

namespace LibraryWebApi.DatabaseContextFactory;

[DbType(Name = "mssql")]
public sealed class MsSqlDbContext : BaseDbContext
{
    public override Compiler GetCompiler()
    {
        return new SqlServerCompiler();
    }

    public override IDbConnection GetConnection(DatabaseOptions dbOptions)
    {
        return new SqlConnection(dbOptions.ConnectionString);
    }
}