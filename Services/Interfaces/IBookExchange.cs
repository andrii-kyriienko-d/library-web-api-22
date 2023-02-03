using LibraryWebApi.Entities;
using System.Collections.Generic;

namespace LibraryWebApi.Services.Interfaces;

public interface IBookExchange
{
    public IEnumerable<Readers> GetReadersForBook(int id);
    public void GiveBookToReader(int id, Readers reader);
    public IEnumerable<Books> GetBooks(int id);
    public void GiveBookToReaderById(int id, Books book);
}