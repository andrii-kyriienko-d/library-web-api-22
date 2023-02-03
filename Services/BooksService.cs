using LibraryWebApi.Entities;
using LibraryWebApi.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryWebApi.Services;

internal sealed class BooksService : BaseService<Books>
{
    public BooksService(IRepository<Books> booksRepository, IMemoryCache cache) 
        : base(booksRepository,cache)
    {
    }
}