using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.DTO;
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

            CreateMap<Book, BookBasicInfoRequest>();
            CreateMap<BookBasicInfoRequest, Book>();



            //CreateMap<PublicPageRequest, PersonalPage>();
            //CreateMap<BookPricesRequest, BookPrice>();
            //CreateMap<BookFormSubRequest, Book>();
        }
    }
}
