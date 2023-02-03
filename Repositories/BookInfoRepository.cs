using LibraryWebApi.Attributes;
using LibraryWebApi.Entities.Interfaces;
using LibraryWebApi.DatabaseContextFactory.Interfaces;
using LibraryWebApi.Models.ResponseModel;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Resolvers.Interfaces;
using Microsoft.Extensions.Options;
using SqlKata.Execution;
using System;
using System.Linq;
using LibraryWebApi.Entities;
using LibraryWebApi.Models.OptionsModels;

namespace LibraryWebApi.Repositories;

internal sealed class BookInfoRepository : IBookInfo
{
    private readonly DatabaseOptions _dbOptions;
    private readonly IDbContext _context;
    public BookInfoRepository(IOptions<DatabaseOptions> dbOptions, IDbContextResolver context)
    {
        _dbOptions = dbOptions.Value;
        _context = context.Resolve(dbOptions.Value.DBMSType);
    }

    public FullBookInfoModel GetBookInfo(int id)
    {
        using var connection = _context.GetConnection(_dbOptions);
        var compiler = _context.GetCompiler();
        var db = new QueryFactory(connection, compiler);

        var booksTableName = (DbTableNameAttribute)Attribute
            .GetCustomAttribute(typeof(Books), typeof(DbTableNameAttribute));
        var bookletTableName = (DbTableNameAttribute)Attribute
            .GetCustomAttribute(typeof(Booklets), typeof(DbTableNameAttribute));

        var query = db.Query(booksTableName.TableName).Join(bookletTableName.TableName,
                $"{ booksTableName.TableName}.{ nameof(Books.BookletId)}",
                $"{ bookletTableName.TableName}.{ nameof(IEntityId.Id)}")
            .Where($"{ booksTableName.TableName}.{ nameof(IEntityId.Id)}", id);

        return query.Get<FullBookInfoModel>().AsEnumerable<FullBookInfoModel>().FirstOrDefault();
    }
}