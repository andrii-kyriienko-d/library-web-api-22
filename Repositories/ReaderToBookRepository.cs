using LibraryWebApi.Attributes;
using LibraryWebApi.Entities;
using LibraryWebApi.Entities.Interfaces;
using LibraryWebApi.Models.OptionsModels;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Resolvers.Interfaces;
using Microsoft.Extensions.Options;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWebApi.Repositories;

internal sealed class ReaderToBookRepository : BaseRepository<ReaderBook>, IReaderBookRepository
{
    public ReaderToBookRepository(IOptions<DatabaseOptions> options, IDbContextResolver dbContext) : base(options, dbContext)
    {
    }

    public IEnumerable<Readers> GetReadersForBook(Books entity)
    {
        using var connection = DbContext.GetConnection(DbOptions);
        var compiler =  DbContext.GetCompiler();

        var db = new QueryFactory(connection, compiler);

        var readerTableName = (DbTableNameAttribute)Attribute
            .GetCustomAttribute(typeof(Readers), typeof(DbTableNameAttribute));
        var readerBookTableName = (DbTableNameAttribute)Attribute
            .GetCustomAttribute(typeof(ReaderBook), typeof(DbTableNameAttribute));

        var readersId = db.Query(readerBookTableName.TableName)
            .Select(nameof(ReaderBook.ReaderId))
            .Where(nameof(ReaderBook.BookId), "=", entity.Id)
            .Get<int>().AsEnumerable<int>();

        return db.Query(readerTableName.TableName).Select()
            .WhereIn<int>(nameof(IEntityId.Id), readersId)
            .Get<Readers>()
            .ToList();
    }
    public IEnumerable<Books> GetBooksForReader(Readers entity)
    {
        using var connection = DbContext.GetConnection(DbOptions);
        var compiler = DbContext.GetCompiler();

        var db = new QueryFactory(connection, compiler);
        var readerBooksName = (DbTableNameAttribute)Attribute
            .GetCustomAttribute(typeof(ReaderBook), typeof(DbTableNameAttribute));
        var booksName = (DbTableNameAttribute)Attribute
            .GetCustomAttribute(typeof(Books), typeof(DbTableNameAttribute));

        var booksId = db.Query(readerBooksName.TableName)
            .Select(nameof(ReaderBook.BookId))
            .Where(nameof(ReaderBook.ReaderId), "=", entity.Id)
            .Get<int>().AsEnumerable<int>();
        var result = db.Query(booksName.TableName).Select()
            .WhereIn<int>(nameof(IEntityId.Id), booksId)
            .Get<Books>()
            .ToList();
        return result;
    }

}