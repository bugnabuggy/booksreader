using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Exceptions;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Services.Author;
using BooksReader.Dictionaries;
using BooksReader.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MessageStrings = BooksReader.Dictionaries.Messages.MessageStrings;

namespace BooksReader.Services
{
    public class AuthorProfileService : IAuthorProfileService
    {
        private readonly IRepository<AuthorProfile> _authorProfileRepo;
        private readonly IRepository<PersonalPage> _personalPagesRepo;

        public AuthorProfileService(IRepository<AuthorProfile> authorProfileRepo,
            IRepository<PersonalPage> personalPagesRepo)
        {
            this._authorProfileRepo = authorProfileRepo;
            this._personalPagesRepo = personalPagesRepo;
        }

        public async Task<OperationResult<AuthorProfile>> CreateAuthorProfile(BrUser user)
        {
            var result = new OperationResult<AuthorProfile>()
            {
                Messages = new List<string>()
            };

            var profile = _authorProfileRepo.Data.AsNoTracking()
                .FirstOrDefault(x => x.User.UserName.Equals(user.UserName));

            if (profile != null)
            {
                result.Data = profile;
                result.Messages.Add(MessageStrings.AuthorAlreadyExists);
                return result;
            }

            profile = new AuthorProfile()
            {
                UserId = user.Id,
                AuthorName = user.Name
            };

            var authorProfile = await _authorProfileRepo.AddAsync(profile);

            result = new OperationResult<AuthorProfile>()
            {
                Success = authorProfile != null,
                Messages = new List<string>(),
                Data = authorProfile
            };

            return result;
        }

        public AuthorProfile GetAuthorProfile(BrUser user)
        {
            var authorProfile = _authorProfileRepo
                .Data
                .Include(x => x.PersonalPage)
                .FirstOrDefault(x => x.UserId.Equals(user.Id));
                
            return authorProfile;
        }

        public OperationResult<AuthorProfile> EditAuthorProfile(AuthorProfileDto profile)
        {
            var result = new OperationResult<AuthorProfile>()
            {
                Messages =  new List<string>(),
                Data = new AuthorProfile()
                {
                    Id = profile.Id?? Guid.Empty,
                    AuthorName = profile.AuthorName,
                    Description = profile.Description,
                }
            };

            var existing = _authorProfileRepo.Data.FirstOrDefault(x => x.Id.Equals(profile.Id));

            if (existing == null)
            {
                result.Messages.Add(MessageStrings.AuthorNotFound);
                goto end;
            }

            if (string.IsNullOrWhiteSpace(profile.AuthorName))
            {
                result.Messages.Add(MessageStrings.CantBeEmpty);
                goto end;
            }

            if (!string.IsNullOrWhiteSpace(profile.DomainName))
            {
                var pageWithDomain = _personalPagesRepo.Data.FirstOrDefault(x =>
                    string.Equals(profile.DomainName,
                                    x.Domain,
                                    StringComparison.InvariantCultureIgnoreCase)
                    && existing.PersonalPageId != x.Id);

                if (pageWithDomain != null)
                {
                    result.Messages.Add(MessageStrings.DomainAlreadyInUse);
                    goto end;
                };
            }


            var personalPage = _personalPagesRepo.Data.FirstOrDefault(x => x.Id.Equals(existing.PersonalPageId));

            // add or update personal page if domain name is not empty
            if (!string.IsNullOrWhiteSpace(profile.DomainName))
            {
                personalPage = new PersonalPage()
                {
                    Id = personalPage?.Id ?? Guid.Empty,
                    SeoInfoId = personalPage?.SeoInfoId ?? null,
                    Content = profile.PageContent,
                    Domain = profile.DomainName,
                    UrlPath = profile.UrlPath,
                    PageType = PersonalPageType.AuthorPage,
                    SubjectId = profile.Id
                };

                personalPage = personalPage.Id == Guid.Empty 
                    ? _personalPagesRepo.Add(personalPage) 
                    : _personalPagesRepo.Update(personalPage);
            }
            else // delete personal page record if there is no domain in request
            {
                if (personalPage != null)
                {
                    _personalPagesRepo.Delete(personalPage);
                }
            }

            // update the author profile
            // TODO: consider replace with Automapper
            existing.Description = profile.Description;
            existing.AuthorName = profile.AuthorName;
            existing.PersonalPageId = personalPage?.Id;

            _authorProfileRepo.Update(existing);

            result.Success = true;
            result.Data = existing;

            end:

            return result;
        }

        public Task<OperationResult<AuthorProfile>> DeleteAuthorProfile(AuthorProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}
