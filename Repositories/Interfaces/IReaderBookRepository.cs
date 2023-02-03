using LibraryWebApi.Entities;
using System.Collections.Generic;

namespace LibraryWebApi.Repositories.Interfaces;

internal interface IReaderBookRepository : IRepository<ReaderBook>
{
    IEnumerable<Readers> GetReadersForBook(Books entity);
    IEnumerable<Books> GetBooksForReader(Readers entity);
}