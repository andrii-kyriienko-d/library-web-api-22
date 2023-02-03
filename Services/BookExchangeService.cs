using LibraryWebApi.Entities;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Services.Interfaces;
using System.Collections.Generic;

namespace LibraryWebApi.Services;

internal sealed class BookExchangeService : IBookExchange
{
    private readonly IReaderBookRepository _readerBookRepository;

    public BookExchangeService(IReaderBookRepository readerBookRepository)
    {
        _readerBookRepository = readerBookRepository;
    }

    public IEnumerable<Readers> GetReadersForBook(int id)
    {
        return _readerBookRepository.GetReadersForBook(new Books() { Id = id });
    }
    public void GiveBookToReader(int id, Readers reader)
    {

        _readerBookRepository.Create(new ReaderBook()
        {
            BookId = id,
            ReaderId = reader.Id
        });
    }
    public IEnumerable<Books> GetBooks(int id)
    {
        return _readerBookRepository.GetBooksForReader(new Readers() { Id = id });
    }
    public void GiveBookToReaderById(int id, Books book)
    {
        _readerBookRepository.Create(new ReaderBook()
        {
            BookId = book.Id,
            ReaderId = id
        });
    }
}