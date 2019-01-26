using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace BooksReader.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<BrUser> _userManager;

        public UsersService(UserManager<BrUser> userManager)
        {
            _userManager = userManager;
        }

        public WebResult<IEnumerable<UserResult>> GetUsers()
        {
            var users = _userManager.Users.Select(x =>
                new UserResult
                {
                    UserName = x.UserName,
                    Name = x.Name,
                    Roles = _userManager.GetRolesAsync(x).Result
                }).ToList();

            var result = new WebResult<IEnumerable<UserResult>>
            {
                  Data = users,
                  Total = users.Count
            };

            return result;
        }
    }
}
