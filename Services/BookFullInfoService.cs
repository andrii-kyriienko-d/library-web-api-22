using AutoMapper;
using LibraryWebApi.Models.ResponseModel;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Services.Interfaces;

namespace LibraryWebApi.Services;

internal sealed class BookFullInfoService : IBookFullInfo
{
    private readonly IMapper _mapper;
    private readonly IBookInfo _bookInfoRepository;

    public BookFullInfoService(IMapper mapper, IBookInfo bookInfoRepository)
    {
        _mapper = mapper;
        _bookInfoRepository = bookInfoRepository;
    }

    public BooksViewModel GetFullInfo(int id)
    {
        var info = _bookInfoRepository.GetBookInfo(id);
        return _mapper.Map<BooksViewModel>(info);
    }
}