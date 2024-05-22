using AutoMapper;
using BookLibrary.Api.Models;
using BookLibrary.Core.Entities;

namespace BookLibrary.Api.MappingProfiles;

public class DataTransferObjectsMappingProfile : Profile
{
    public DataTransferObjectsMappingProfile()
    {
        CreateMap<Book, BookSearchDataTransferObject>()
            .ForMember(p => p.Id, m => m.MapFrom(s => s.Id))
            .ForMember(p => p.BookTitle, m => m.MapFrom(s => s.Title))
            .ForMember(p => p.Publisher, m => m.MapFrom(s => s.Publisher.Name))
            .ForMember(p => p.Authors, m => m.MapFrom(s => s.Author.Name))
            .ForMember(p => p.Type, m => m.MapFrom(s => s.Type))
            .ForMember(p => p.ISBN, m => m.MapFrom(s => s.ISBN))
            .ForMember(p => p.Category, m => m.MapFrom(s => s.Category))
            .ForMember(p => p.AvailableCopies, m => m.MapFrom(s => $"{s.CopiesInUse}/{s.TotalCopies}"))
            .ForMember(p => p.TotalItemCount, m => m.MapFrom(s => s.TotalItemCount));

        CreateMap<Book, BookDataTransferObject>().ReverseMap();

        CreateMap<Author, AuthorDataTransferObject>().ReverseMap();
        CreateMap<Publisher, PublisherDataTransferObject>().ReverseMap();
    }
}