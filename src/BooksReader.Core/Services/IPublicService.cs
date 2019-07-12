using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;

namespace BooksReader.Core.Services
{
    public interface IPublicService
    {
        PublicPageInfo GetInfo(PublicPageInfoRequest info);
    }
}
