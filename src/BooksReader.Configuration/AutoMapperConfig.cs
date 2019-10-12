using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;

namespace BooksReader.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserDomainRequest, UserDomain>();
            CreateMap<UserDomain, UserDomainDto>();
            
            //CreateMap<PublicPageRequest, PersonalPage>();
            //CreateMap<BookPricesRequest, BookPrice>();
            //CreateMap<BookFormSubRequest, Book>();
        }
    }
}
