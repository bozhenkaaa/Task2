using AutoMapper;
using Task2.DTOs;
using Task2.Models;

public class BookStoreMappingProfile : Profile
{
    public BookStoreMappingProfile()
    {
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Genre, GenreDto>().ReverseMap();
    }
}