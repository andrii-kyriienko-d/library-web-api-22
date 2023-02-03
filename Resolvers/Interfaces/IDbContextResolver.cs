using LibraryWebApi.DatabaseContextFactory.Interfaces;

namespace LibraryWebApi.Resolvers.Interfaces;

internal interface IDbContextResolver
{
    IDbContext Resolve(string dbType);
}