using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class AuthorProfileService : IAuthorProfileService
    {
        private readonly IRepository<AuthorProfile> _authorProfileRepo;

        public AuthorProfileService(
            IRepository<AuthorProfile> authorProfileRepo)
        {
            _authorProfileRepo = authorProfileRepo;
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
    }
}
