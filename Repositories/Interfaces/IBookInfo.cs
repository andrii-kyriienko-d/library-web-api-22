using LibraryWebApi.Models.ResponseModel;

namespace LibraryWebApi.Repositories.Interfaces;

internal interface IBookInfo
{
    FullBookInfoModel GetBookInfo(int id);
}