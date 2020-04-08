using AutoMapper;
using Books.Domain.Entities;
using Books.WebApi.DTOs;
using System.Collections.Generic;

namespace BooksServicesWebApi.MapperDTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>(); 
            CreateMap<BookDTO, Book>();
        }
    }
}
