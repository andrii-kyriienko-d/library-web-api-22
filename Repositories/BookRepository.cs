using LibraryWebApi.Entities;
using LibraryWebApi.Models.OptionsModels;
using LibraryWebApi.Resolvers.Interfaces;
using Microsoft.Extensions.Options;

namespace LibraryWebApi.Repositories;

internal sealed class BookRepository : BaseRepository<Books>
{
    public BookRepository(IOptions<DatabaseOptions> options, IDbContextResolver dbContext) : base(options, dbContext)
    {
    }
}