using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.DTO.Admin;
using BooksReader.Core.Models.DTO.Public;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Models.Requests.Author;

namespace BooksReader.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserDomainRequest, UserDomain>();
            CreateMap<UserDomain, UserDomainDto>();

            // book editing models
            CreateMap<Book, BookBasicInfoRequest>();
            CreateMap<Book, AdminBookDto>()
                .ForMember(x => x.BookTitle, opt => opt.MapFrom(y => y.Title))
                .ForMember(x => x.BookId, opt => opt.MapFrom(y => y.Id));

            CreateMap<Book, BookMarketDto>();

            CreateMap<BookBasicInfoRequest, Book>();
            CreateMap<BookEditRequest, Book>();
            

            CreateMap<BookPriceRequest, BookPrice>();

            CreateMap<BookChapterRequest, BookChapter>();
            

        }
    }
}
