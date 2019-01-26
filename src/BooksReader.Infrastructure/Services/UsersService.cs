using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Infrastructure.Models;
using BooksReader.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BooksReader.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly BrDbContext _ctx;

        public UsersService(BrDbContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<UserResult> GetUsersWithRoles()
        {
            var users = _ctx.Users
                .Join(_ctx.UserRoles, x => x.Id, y => y.UserId,
                    (user, userRole) => new {user, userRole})
                .Join(_ctx.Roles, x => x.userRole.RoleId, y => y.Id,
                    (userRole, role) => new {User = userRole.user, Role = role})
                .GroupBy(x => x.User).Select(x => new UserResult
                {
                    UserName = x.Key.UserName,
                    Name =  x.Key.Name,
                    Roles = x.Select(y=>y.Role.Name)
                });

            return users;
        }
    }
}
