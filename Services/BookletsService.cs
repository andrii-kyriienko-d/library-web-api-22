using LibraryWebApi.Entities;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryWebApi.Services;

internal sealed class BookletsService : BaseService<Booklets>
{
    public BookletsService(IRepository<Booklets> bookletsRepository, IMemoryCache cache) 
        : base(bookletsRepository,cache)
    {
    }
}