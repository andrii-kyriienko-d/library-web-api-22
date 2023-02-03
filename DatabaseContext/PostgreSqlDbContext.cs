using LibraryWebApi.Attributes;
using LibraryWebApi.Models.OptionsModels;
using Npgsql;
using SqlKata.Compilers;
using System.Data;

namespace LibraryWebApi.DatabaseContextFactory;

[DbType(Name = "postgresql")]
internal sealed class PostgreSqlDbContext : BaseDbContext
{
    public override Compiler GetCompiler()
    {
        return new PostgresCompiler();
    }

    public override IDbConnection GetConnection(DatabaseOptions dbOptions)
    {
        return new NpgsqlConnection(dbOptions.ConnectionString);
    }
}