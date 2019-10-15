using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class PublicPageService : IPublicPagesService
    {
        private readonly IRepository<PublicPage> _pagesRepo;
        private readonly IRepository<Book> _booksRepo;
        private readonly ISecurityService _security;

        public PublicPageService(
            IRepository<PublicPage> pagesRepo,
            IRepository<Book> booksRepo,
            ISecurityService security
            )
        {
            _pagesRepo = pagesRepo;
            _booksRepo = booksRepo;
            _security = security;
        }

        private readonly IEnumerable<Func<PublicPage, PublicPageService, BrUser, string>> _validations =
            new List<Func<PublicPage, PublicPageService, BrUser, string>>()
            {
                // page content must not be empty
                (page, svc, user) =>
                {
                    var msg = string.IsNullOrWhiteSpace(page.Content)
                        ? MessageStrings.PublicPageMessages.ContentCantBeEmpty
                        : "";
                    return msg;
                },

                // domain must be filled
                (page, svc, user) =>
                {
                    var msg = page.DomainId == Guid.Empty
                        ? MessageStrings.PublicPageMessages.DomainCantBeEmpty
                        : "";
                    return msg;
                },

                // check if user already has a public page and tries add new one
                (page, svc, user) =>
                {
                    if (page.PageType == PublicPageType.AuthorPage
                        && page.Id == Guid.Empty
                        )
                    {
                        var subjectId = page.SubjectId.HasValue
                            ? page.SubjectId.Value
                            : user.Id;

                        var exists = svc._pagesRepo.Data.AsNoTracking()
                            .Any(x => x.PageType == PublicPageType.AuthorPage && subjectId.Equals(x.SubjectId));

                        if (exists) return MessageStrings.PublicPageMessages.AuthorAlreadyHasPublicPage;
                    }

                    return "";  
                },

                // check if user is owner or admin of domain record
                (page, svc, user) =>
                {
                    var hasAccess = true;
                    // for author personal page
                    if (page.PageType == PublicPageType.AuthorPage)
                    {
                        hasAccess = svc._security.IsInRole(user.Id, SiteRoles.Admin)
                                    || user.Id.Equals(page.SubjectId);
                    }

                    // for book page
                    if (page.PageType == PublicPageType.BookPage)
                    {
                        var book = svc._booksRepo.Data.AsNoTracking()
                            .FirstOrDefault(x => x.Id.Equals(page.SubjectId));
                        
                        if (book == null) 
                            return MessageStrings.PublicPageMessages.SubjectForThePageDoesNotExists;

                        hasAccess = book.OwnerId.Equals(user.Id) 
                            || svc._security.IsInRole(user.Id, SiteRoles.Admin);
                    }

                    // for promo page?

                    var msg = !hasAccess
                        ? MessageStrings.DoNotHavePermissions
                        : "";

                    return msg;
                }
            };


        public IOperationResult<PublicPage> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IOperationResult<PublicPage> Add(PublicPage page, BrUser actingUser)
        {
            if (page.PageType == PublicPageType.AuthorPage && !page.SubjectId.HasValue)
            {
                page.SubjectId = actingUser.Id;
            }

            var result = Validate(page, actingUser);
            if (!result.Success)
            {
                return result;
            }

            page = _pagesRepo.Add(page);

            result.Data = page;
            result.Success = true;

            return result;
        }

        public IOperationResult<PublicPage> Update(PublicPage page, BrUser actingUser)
        {
            var result = Validate(page, actingUser);

            if (!result.Success)
            {
                return result;
            }

            var entity = _pagesRepo.Data.FirstOrDefault(x=>x.Id.Equals(page.Id));

            // todo: here will be seo info manipulations

            // todo: change to use automapper 
            entity.DomainId = page.DomainId;
            entity.PageType = page.PageType;
            entity.UrlPath = page.UrlPath;
            entity.Content = page.Content;

            entity = _pagesRepo.Update(entity);

            result.Data = entity;
            result.Success = true;

            return result;
        }

        public IOperationResult<PublicPage> Delete(Guid pageId, BrUser actingUser)
        {
            var page = _pagesRepo.Data.FirstOrDefault(x => x.Id.Equals(pageId));

            var isAdmin = _security.IsInRole(actingUser.Id, SiteRoles.Admin);
            var canDelete = false;

            var result = new OperationResult<PublicPage>(page);
            
            // for author page user must be an owner
            if (page.PageType == PublicPageType.AuthorPage)
            {
                canDelete = actingUser.Id.Equals(page.SubjectId);
            }

            // for book page, book must be owned by user, or not exists
            if (page.PageType == PublicPageType.BookPage)
            {
                var book = _booksRepo.Data.AsNoTracking()
                    .FirstOrDefault(x => x.Id.Equals(page.SubjectId));

                canDelete = book?.OwnerId.Equals(actingUser.Id) ?? true;
            }

            if (canDelete || isAdmin)
            {
                _pagesRepo.Delete(page);
                result.Success = true;
            }
            else
            {
                result.Messages.Add(MessageStrings.DoNotHavePermissions);
            }

            return result;
        }

        private IOperationResult<PublicPage> Validate(PublicPage page, BrUser actingUser)
        {
            var result = new OperationResult<PublicPage>();

            var validations = _validations.Select(x => x(page, this, actingUser))
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList();

            if (validations.Any())
            {
                result.Messages = validations;
                return result;
            }

            result.Success = true;
            return result;
        }
    }
}
