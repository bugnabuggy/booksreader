using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Author;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class AuthorProfileService : IAuthorProfileService
    {
        private readonly IRepository<AuthorProfile> _authorProfileRepo;
        private readonly IRepository<UserDomain> _domainsRepo;
        private readonly IRepository<PublicPage> _pagesRepo;

        public AuthorProfileService(
            IRepository<AuthorProfile> authorProfileRepo,
            IRepository<UserDomain> domainsRepo,
            IRepository<PublicPage> pagesRepo
            )
        {
            _authorProfileRepo = authorProfileRepo;
            _domainsRepo = domainsRepo;
            _pagesRepo = pagesRepo;
        }

        

        public async Task<OperationResult<AuthorProfile>> CreateAuthorProfile(BrUser user)
        {
            var result = new OperationResult<AuthorProfile>(){};

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
            
            // dont know how to disable loading of navigation properties
            authorProfile.User = null;

            result = new OperationResult<AuthorProfile>()
            {
                Success = true,
                Data = authorProfile
            };

            return result;
        }

        public Task<OperationResult<AuthorProfile>> EditAuthorProfile(AuthorProfileDto profile)
        {
            throw new NotImplementedException();
        }

        public AuthorProfile GetAuthorProfile(Guid brUserId)
        {
            var authorProfile = _authorProfileRepo
                .Data.AsNoTracking()
                .FirstOrDefault(x => x.UserId.Equals(brUserId));

            return authorProfile;
        }

        public AuthorProfileDto GetAuthorFullProfile(Guid brUserId)
        {
            var authorProfile = _authorProfileRepo
                .Data.AsNoTracking()
                .FirstOrDefault(x => x.UserId.Equals(brUserId));

            if (authorProfile == null)
            {
                return null;
            }

            var domains = _domainsRepo.Data.AsNoTracking()
                .Where(x => x.OwnerId.Equals(brUserId));

            var page = _pagesRepo.Data.AsNoTracking()
                .FirstOrDefault(x => x.SubjectId.Equals(brUserId) 
                && x.PageType == PublicPageType.AuthorPage);

            return new AuthorProfileDto()
            {
                Id = authorProfile.Id,
                Description = authorProfile.Description,
                AuthorName =  authorProfile.AuthorName,
                Domains = domains,
                Page =  page
            };
        }

    }
}
