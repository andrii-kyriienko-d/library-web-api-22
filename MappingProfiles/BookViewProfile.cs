using AutoMapper;
using LibraryWebApi.Models.ResponseModel;

namespace LibraryWebApi.MappingProfiles;

internal sealed class BookViewProfile : Profile
{
    public BookViewProfile()
    {
        CreateMap<FullBookInfoModel, BooksViewModel>()
            .ForMember(dest => dest.FullName, model => model.MapFrom(src => (src.FullName + "/" + src.Name)))
            .ForMember(dest => dest.Description, model => model.MapFrom(src => src.Description))
            .ForMember(dest => dest.Genre, model => model.MapFrom(src => src.Genre))
            .ForMember(dest => dest.Link, model => model.MapFrom(src => src.WikiLink))
            .ForMember(dest => dest.FullName, model => model.MapFrom(src => src.FullName))
            .ForMember(dest => dest.FullName, model => model.MapFrom(src => src.FullName));
    }
}