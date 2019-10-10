using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests.User;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using BooksReader.Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;

namespace BooksReader.Infrastructure.Services
{
    public class UsersService: IUsersService
    {
        private Dictionary<string, Expression<Func<LoginHistory, object>>> orderBys = new Dictionary<string, Expression<Func<LoginHistory, object>>>()
        {
            {"dateTime", x => x.DateTime},
            {"browser", x=>x.Browser},
            {"geolocation", x=>x.Geolocation},
            {"ipAddress", x=>x.IpAddress},
            {"Screen", x=>x.Screen},
            {"Id", x=>x.Id},
            {"userId", x=>x.UserId}
        };

        private readonly BrDbContext _ctx;
        private readonly UserManager<BrUser> _userManager;
        private readonly IRepository<LoginHistory> _loginHistory;

        public UsersService(
            BrDbContext ctx,
            UserManager<BrUser> userManager,
            IRepository<LoginHistory> logHistory)
        {
            _ctx = ctx;
            _loginHistory = logHistory;
            _userManager = userManager;
        }

        public async Task<LoginHistory> AddLoginHistory(LoginHistory logHistory, Guid userId)
        {
            var result = await this._loginHistory.AddAsync(new LoginHistory
            {
                Id = Guid.NewGuid(),
                DateTime = logHistory.DateTime,
                IpAddress = logHistory.IpAddress,
                Browser = logHistory.Browser,
                UserId = userId,
                Geolocation = logHistory.Geolocation
            });

            return result;
        }

        public IQueryable<LoginHistory> GetLoginHistory(StandardFilters filters, Guid userId, out int totalItems)
        {
            IQueryable<LoginHistory> data = _loginHistory.Data;

            filters.PageNumber = filters.PageNumber ?? 0;

            totalItems = data.Count();

            if (!string.IsNullOrWhiteSpace(filters.OrderByField)
                && orderBys.ContainsKey(filters.OrderByField))
            {
                var orderExp = orderBys[filters.OrderByField];

                if (filters.IsDesc.HasValue)
                {
                    data = filters.IsDesc.Value
                        ? data.OrderByDescending(orderExp)
                        : data.OrderBy(orderExp);
                }
            }

            data = filters.PageSize == null
                ? data
                : data.Skip((int)(filters.PageNumber)
                            * (int)filters.PageSize)
                    .Take((int)filters.PageSize);

            return data;
        }

        public async Task<OperationResult<AppUserDto>> Update(UserProfileRequest data)
        {
            var result = new OperationResult<AppUserDto>();

            var user = await _userManager.FindByNameAsync(data.Username);
            if (user == null)
            {
                result.Messages.Add(MessageStrings.UserDoesNotExist);
                return result;
            }

            //TODO: consider to use mappers
            user.Name = data.Name;
            user.Avatar = data.Avatar;
            user.Email = data.Email;

            var identityResult = await _userManager.UpdateAsync(user);
            result.Success = identityResult.Succeeded;
            result.Messages = identityResult.Errors.Select(x => x.Code).ToList();

            //TODO: consider to use mappers
            result.Data = new AppUserDto()
            {
                Name = user.Name,
                Username = user.UserName,
                Avatar = user.Avatar,
                Id = user.Id.ToString(),
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return result;
        }

        public async Task<OperationResult<AppUserDto>> Delete(string username)
        {
            var result = new OperationResult<AppUserDto>();
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                result.Success = false;
                result.Messages = new[] { MessageStrings.UserDoesNotExist };
                return result;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var identityResult = await _userManager.DeleteAsync(user);

            result.Success = identityResult.Succeeded;
            result.Messages = identityResult.Errors.Select(x => x.Code).ToList();
            result.Data = new AppUserDto()
            {
                Name = user.Name,
                Avatar = user.Avatar,
                Email = user.Email,
                Username = user.UserName,
                Id = user.Id.ToString(),
                Roles = roles
            };

            return result;
        }

        public IQueryable<UserResult> GetUsersWithRoles()
        {
            var users = _ctx.Users
                .GroupJoin(_ctx.UserRoles, x => x.Id, y => y.UserId,
                    (user, userRole) => new { user, userRole })
                .SelectMany(x => x.userRole.DefaultIfEmpty(), (x, y) => new { x.user, userRole = y })
                .GroupJoin(_ctx.Roles, x => x.userRole.RoleId, y => y.Id, (x, y) => new { x.user, role = y })
                .SelectMany(x => x.role.DefaultIfEmpty(), (x, y) => new { User = x.user, Role = y })
                .GroupBy(x => x.User)
                .Select(x => new UserResult
                {
                    Username = x.Key.UserName,
                    Name = x.Key.Name,
                    Roles = x.Select(y => y.Role == null ? null : y.Role.Name).Where(z => z != null)
                });

            return users;
        }

        public async Task<OperationResult> AddUserRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            IdentityResult result = IdentityResult.Success;

            if (!await _userManager.IsInRoleAsync(user, role))
            {
                result = await _userManager.AddToRoleAsync(user, role);
            }

            return new OperationResult()
            {
                Success = result.Succeeded,
                Messages = result.Errors.Select(x => x.Description).ToList()
            };
        }

        public async Task<OperationResult> RemoveUserRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            IdentityResult result = IdentityResult.Success;

            if (await _userManager.IsInRoleAsync(user, role))
            {
                result = await _userManager.RemoveFromRoleAsync(user, role);
            }

            return new OperationResult()
            {
                Success = result.Succeeded,
                Messages = result.Errors.Select(x => x.Description).ToList()
            };
        }

        public async Task<OperationResult> ToggleUserRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            IdentityResult result;
            if (await _userManager.IsInRoleAsync(user, role))
            {
                result = await _userManager.RemoveFromRoleAsync(user, role);
            }
            else
            {
                result = await _userManager.AddToRoleAsync(user, role);
            }

            return new OperationResult()
            {
                Messages = result.Errors.Select(x => x.Description).ToList(),
                Success = result.Succeeded
            };
        }
    }
}
