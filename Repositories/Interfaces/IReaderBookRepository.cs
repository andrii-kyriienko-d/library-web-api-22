using LibraryWebApi.Entities;
using System.Collections.Generic;

namespace LibraryWebApi.Repositories.Interfaces;

internal interface IReaderBookRepository : IRepository<ReaderBook>
{
    public List<Readers> GetReadersForBook(Books entity);
    public List<Books> GetBooksForReader(Readers entity);
}