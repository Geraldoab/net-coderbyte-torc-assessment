using AutoMapper;
using BookLibrary.Api.Models;
using BookLibrary.Core.Entities;

namespace BookLibrary.Api.MappingProfiles;

public class DataTransferObjectsMappingProfile : Profile
{
    public DataTransferObjectsMappingProfile()
    {
        CreateMap<Book, BookDataTransferObject>().ReverseMap();
    }
}