using LibraryWebApi.Models.ResponseModel;

namespace LibraryWebApi.Services.Interfaces;

public interface IBookFullInfo
{
    BooksViewModel GetFullInfo(int id);
}