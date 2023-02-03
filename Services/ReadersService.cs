using LibraryWebApi.Entities;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryWebApi.Services;

internal sealed class ReadersService : BaseService<Readers>, IBaseService<Readers>
{
    public ReadersService(IRepository<Readers> readersRepository, IMemoryCache cache)
        : base(readersRepository,cache)
    {
    }
}