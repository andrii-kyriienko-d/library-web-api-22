using LibraryWebApi.Attributes;
using LibraryWebApi.Entities.Interfaces;
using LibraryWebApi.DatabaseContextFactory.Interfaces;
using LibraryWebApi.Models.RequestModels;
using LibraryWebApi.Resolvers.Interfaces;
using Microsoft.Extensions.Options;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LibraryWebApi.Models.OptionsModels;
using LibraryWebApi.Repositories.Interfaces;

namespace LibraryWebApi.Repositories;

internal abstract class BaseRepository<T> : IRepository<T>
    where T : class, IEntityId, IEntityDateInfo
{
    protected readonly DatabaseOptions DbOptions;
    protected readonly IDbContext DbContext;

    protected BaseRepository(IOptions<DatabaseOptions> dbOptions, IDbContextResolver dbContext)
    {
        DbOptions = dbOptions.Value;
        DbContext = dbContext.Resolve(dbOptions.Value.DBMSType);
    }

    public int Create(T entity)
    {
        using var connection = DbContext.GetConnection(DbOptions);
        var compiler = DbContext.GetCompiler();
        var db = new QueryFactory(connection, compiler);

        entity.Id = GetLastId(db, GetTableName()) + 1;

        entity.Created = DateTime.SpecifyKind(
            DateTime.Now.ToUniversalTime(),
            DateTimeKind.Utc
        );
        entity.Modified = DateTime.SpecifyKind(
            DateTime.Now.ToUniversalTime(),
            DateTimeKind.Utc
        );
        try
        {
            db.Query(GetTableName())
                .Insert(entity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return -1;
        }

        return entity.Id;

    }

    private int GetLastId(QueryFactory db, string tableName)
    {
        var records = db.Query(tableName)
            .Select(nameof(IEntityId.Id))
            .Get<T>().ToList();
        return records.Any() ? records.Max(item => item.Id) : default;
    }

    private string GetTableName()
    {
        var attribute = (DbTableNameAttribute)Attribute
            .GetCustomAttribute(typeof(T), typeof(DbTableNameAttribute));
        return attribute.TableName;
    }

    public void Delete(int id)
    {
        using var connection = DbContext.GetConnection(DbOptions);
        var compiler = DbContext.GetCompiler();
        var db = new QueryFactory(connection, compiler);
            
        db.Query(GetTableName())
            .Where(nameof(IEntityId.Id), "=", id)
            .Delete();
    }

    public T Get(int id)
    {
        using var connection = DbContext.GetConnection(DbOptions);
        var compiler = DbContext.GetCompiler();
        var db = new QueryFactory(connection, compiler);
           
        return db.Query(GetTableName())
            .Select()
            .Where(nameof(IEntityId.Id), "=", id)
            .FirstOrDefault<T>();
    }
    public IEnumerable<T> GetEntities()
    {
        using var connection = DbContext.GetConnection(DbOptions);
        var compiler = DbContext.GetCompiler();
        var db = new QueryFactory(connection, compiler);
        return db.Query(GetTableName())
            .Select()
            .Get<T>();
    }

    public void Update(T entity)
    {
        using var connection = DbContext.GetConnection(DbOptions);
        var compiler = DbContext.GetCompiler();
        var db = new QueryFactory(connection, compiler);
          
        entity.Created = new DateTime(Get(entity.Id).Created.Ticks);

        entity.Modified = DateTime.SpecifyKind(
            DateTime.Now.ToUniversalTime(),
            DateTimeKind.Utc
        );

        var queryUpdate = new Query(GetTableName())
            .Where(nameof(IEntityId.Id), "=", entity.Id)
            .AsUpdate(entity);

        SqlResult queryResult = compiler.Compile(queryUpdate);
        db.Statement(queryResult.ToString());
    }

    public IEnumerable<T> Find(SearchModel findExpression)
    {
        using var connection = DbContext.GetConnection(DbOptions);
        var compiler = DbContext.GetCompiler();
        var db = new QueryFactory(connection, compiler);

        Type entityType = typeof(T);

        List<T> items = new List<T>();

        var columns = entityType.GetProperties();
          
        var connectionType =
            (DbTypeAttribute)Attribute.GetCustomAttribute(DbContext.GetType(), typeof(DbTypeAttribute));

        Query findQuery = new Query(GetTableName());
        foreach (var column in columns)
        {

            if (column.PropertyType == typeof(DateTime))
            {
                if (connectionType.Name == "mssql")
                {
                    findQuery = findQuery.Or()
                        .WhereRaw($"CAST( DATEADD (MINUTE,?,CAST([{column.Name}] AS datetime2)) AS varchar) LIKE (?)",
                            new object[]
                            {
                                findExpression.TimesOnOffset,
                                $"%{findExpression.SearchExpression}%"
                            });
                }
                else
                {
                    findQuery = findQuery.Or()
                        .WhereRaw($"(\"{column.Name}\"::timestamp + INTERVAL ?)::text LIKE (?)",
                            new object[]
                            {
                                $"{findExpression.TimesOnOffset}min",
                                $"%{findExpression.SearchExpression}%"
                            });
                }
            }
            else
            {
                if (connectionType.Name == "mssql")
                {
                    findQuery = findQuery.Or()
                        .WhereRaw($"CAST([{column.Name}] AS varchar) LIKE (?)",
                            new object[]
                            {
                                $"%{findExpression.SearchExpression}%"
                            });
                }
                else
                {
                    findQuery = findQuery.Or()
                        .WhereRaw($"CAST(\"{column.Name}\" AS TEXT) LIKE (?)",
                            new object[]
                            {
                                $"%{findExpression.SearchExpression}%"
                            });
                }
            }

        }

        SqlResult finalQueryResult = compiler.Compile(findQuery);

        var result = db.FromQuery(finalQueryResult.Query).Get<T>().AsEnumerable();
        items.AddRange(result);
        return items.AsEnumerable<T>();
    }
}