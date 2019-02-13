using AutoMapper;
using Books.Model;
using Books.Model.DTOs;
using Books.Model.Entities;
using System.Collections.Generic;

namespace BooksServicesWebApi.MapperDTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookEntity, BookDTO>(); 
            CreateMap<BookDTO, BookEntity>();
        }
    }
}
