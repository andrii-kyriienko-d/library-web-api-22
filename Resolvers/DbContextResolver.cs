using LibraryWebApi.Attributes;
using LibraryWebApi.DatabaseContextFactory.Interfaces;
using LibraryWebApi.Resolvers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWebApi.Resolvers;

internal sealed class DbContextResolver : IDbContextResolver
{
    private readonly IEnumerable<IDbContext> _dbContexts;

    public DbContextResolver(IEnumerable<IDbContext> dbcontexts)
    {
        _dbContexts = dbcontexts;
    }

    public IDbContext Resolve(string dbType)
    {
        var context = _dbContexts
            .FirstOrDefault(dbcontext => ((DbTypeAttribute)Attribute
                .GetCustomAttribute(dbcontext.GetType(), typeof(DbTypeAttribute))).Name == dbType);

        if(context == null)
        {
            throw new Exception("DBMS type " + dbType + " not found");
        }
        return context;
    }
}