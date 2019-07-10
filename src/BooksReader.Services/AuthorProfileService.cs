using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Exceptions;
using BooksReader.Core.Models;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services.Author;
using BooksReader.Infrastructure.Repositories;

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
            var profile = _authorProfileRepo.Data
                .FirstOrDefault(x => x.User.UserName.Equals(user.UserName));

            if (profile != null)
            {
                throw new ItemAlreadyExistsException<AuthorProfile>(profile);
            }

            profile = new AuthorProfile()
            {
                UserId = user.Id,
                AuthorName = user.Name
            };

            var authorProfile = await _authorProfileRepo.AddAsync(profile);

            var result = new OperationResult<AuthorProfile>()
            {
                Success = authorProfile != null,
                Messages = new List<string>(),
                Data = authorProfile
            };

            return result;
        }

        public AuthorProfile GetAuthorProfile(BrUser user)
        {
            var authorProfile = _authorProfileRepo.Data.FirstOrDefault(x => x.UserId.Equals(user.Id));
            return authorProfile;
        }

        public AuthorProfile EditAuthorProfile(AuthorProfileRequest profile)
        {
            var existing = _authorProfileRepo.Data.FirstOrDefault(x => x.Id.Equals(profile.ProfileId));

            if (existing == null)
            {
                throw new NotFoundException<AuthorProfile>(new AuthorProfile()
                {
                    Id = profile.ProfileId,
                    AuthorName = profile.AuthorName
                });
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
                    PageType = PersonalPageType.AuthorPage
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

            existing.Description = profile.Description;
            existing.AuthorName = profile.AuthorName;
            existing.PersonalPageId = personalPage?.Id;

            _authorProfileRepo.Update(existing);
            
            return existing;
        }

        public Task<OperationResult<AuthorProfile>> DeleteAuthorProfile(AuthorProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}
