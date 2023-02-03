using LibraryWebApi.Entities;
using System.Collections.Generic;

namespace LibraryWebApi.Services.Interfaces;

public interface IBookExchange
{
    IEnumerable<Readers> GetReadersForBook(int id);
    void GiveBookToReader(int id, Readers reader);
    IEnumerable<Books> GetBooks(int id);
    void GiveBookToReaderById(int id, Books book);
}