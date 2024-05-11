using AutoMapper;
using BookLibrary.Api.Models;
using BookLibrary.Core.Entities;

namespace BookLibrary.Api.MappingProfiles;

public class DataTransferObjectsMappingProfile : Profile
{
    public DataTransferObjectsMappingProfile()
    {
        CreateMap<Book, BookDataTransferObject>()
            .ForMember(p => p.BookTitle, m => m.MapFrom(s => s.Title))
            .ForMember(p => p.Publisher, m => m.MapFrom(s => s.Publisher.Name))
            .ForMember(p => p.Authors, m => m.MapFrom(s => s.Author.Name))
            .ForMember(p => p.Type, m => m.MapFrom(s => s.Type))
            .ForMember(p => p.ISBN, m => m.MapFrom(s => s.ISBN))
            .ForMember(p => p.Category, m => m.MapFrom(s => s.Category))
            .ForMember(p => p.AvailableCopies, m => m.MapFrom(s => $"{s.CopiesInUse}/{s.TotalCopies}"));
    }
}