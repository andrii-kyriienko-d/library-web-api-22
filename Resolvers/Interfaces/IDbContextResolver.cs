using LibraryWebApi.DatabaseContextFactory.Interfaces;

namespace LibraryWebApi.Resolvers.Interfaces;

public interface IDbContextResolver
{
    public IDbContext Resolve(string dbType);
}