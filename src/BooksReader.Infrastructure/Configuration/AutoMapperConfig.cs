using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.Requests;

namespace BooksReader.Infrastructure.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PublicPageRequest, PersonalPage>();
            CreateMap<BookPricesRequest, BookPrice>();
            CreateMap<BookFormSubRequest, Book>();
        }
    }
}
