using LibraryWebApi.Models.ResponseModel;

namespace LibraryWebApi.Services.Interfaces;

public interface IBookFullInfo
{
    public BooksViewModel GetFullInfo(int id);
}