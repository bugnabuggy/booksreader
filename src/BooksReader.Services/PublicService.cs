using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;
using BooksReader.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Core.Services
{
    public class PublicService: IPublicService
    {
        private readonly IRepository<PersonalPage> _personalPagesRepo;

        public PublicService(IRepository<PersonalPage> personalPagesRepo)
        {
            _personalPagesRepo = personalPagesRepo;
        }

        public PublicPageInfo GetInfo(PublicPageInfoRequest info)
        {
            var pages = _personalPagesRepo.Data.Where(x =>
                string.Equals(x.Domain, info.Domain, StringComparison.InvariantCultureIgnoreCase));
            if (!pages.Any())
            {
                return null;
            }

            var page = pages
                .Include(x=>x.SeoInfo)
                .FirstOrDefault(x => string.Equals(x.UrlPath, info.UrlPath, StringComparison.InvariantCultureIgnoreCase));

            if (page != null)
            {
                return new PublicPageInfo()
                {
                    Content = page.Content,
                    SeoInfo = page.SeoInfo
                };
            }

            pages = pages.Include(x => x.SeoInfo);

            page = pages.Any(x => string.IsNullOrEmpty(x.UrlPath))
                ? pages.FirstOrDefault(x => string.IsNullOrEmpty(x.UrlPath))
                : pages.FirstOrDefault();

            return new PublicPageInfo()
            {
                SeoInfo = page.SeoInfo,
                Content = page.Content
            };
        }
    }
}
