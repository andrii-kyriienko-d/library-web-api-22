using LibraryWebApi.Entities;
using LibraryWebApi.Models.OptionsModels;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Resolvers.Interfaces;
using Microsoft.Extensions.Options;

namespace LibraryWebApi.Repositories;

internal sealed class ReaderRepository : BaseRepository<Readers>
{
    public ReaderRepository(IOptions<DatabaseOptions> options, IDbContextResolver dbContext) : base(options, dbContext)
    {
    }
}