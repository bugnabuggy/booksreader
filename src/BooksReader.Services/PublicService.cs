using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class PublicService: IPublicService
    {
        private readonly IRepository<PublicPage> _pagesRepo;
        private readonly IRepository<UserDomain> _domainsRepo;

        public PublicService(
            IRepository<PublicPage> pagesRepo,
            IRepository<UserDomain> domainsRepo)
        {
            _pagesRepo = pagesRepo;
            _domainsRepo = domainsRepo;
        }

        public PublicPageInfoDto GetInfo(PublicPageInfoRequest info)
        {
            // get pages where domain is verified and match requested

            var pages = _pagesRepo.Data.AsNoTracking()
                .Where(x => x.Domain.Verified
                            && x.Domain.Name.Equals(info.Domain,
                                               StringComparison.InvariantCultureIgnoreCase));

            var page = pages
                .Include(x=>x.SeoInfo)
                .FirstOrDefault(x => x.UrlPath.Equals(info.UrlPath));

            if (page != null)
            {
                return new PublicPageInfoDto()
                {
                    Path = page.UrlPath,
                    Content = page.Content,
                    SeoInfo = page.SeoInfo
                };
            }

            page = pages.Any(x => string.IsNullOrWhiteSpace(x.UrlPath))
                ? pages.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.UrlPath))
                : pages.FirstOrDefault();

            if (page == null)
            {
                return null;
            }

            return new PublicPageInfoDto()
            {
                Path = page.UrlPath,
                Content = page.Content,
                SeoInfo = page.SeoInfo
            };
        }
    }
}
