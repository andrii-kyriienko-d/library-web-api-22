using LibraryWebApi.Models.ResponseModel;

namespace LibraryWebApi.Repositories.Interfaces;

internal interface IBookInfo
{
    public FullBookInfoModel GetBookInfo(int id);
}