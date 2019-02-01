using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
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
	    private readonly IRepository<LoginHistory> _logHistory;

        public UsersService(
            BrDbContext ctx,
            UserManager<BrUser> userManager,
            IRepository<LoginHistory> logHistory
            )
        {
            _ctx = ctx;
	        _logHistory = logHistory;
            _userManager = userManager;
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
            
            if (! await _userManager.IsInRoleAsync(user, role))
            {
                result = await _userManager.AddToRoleAsync(user, role);
            }
            
            return new OperationResult()
            {
                Success = result.Succeeded,
                Messages =  result.Errors.Select(x=>x.Description).ToList()
            };
        }

        public async Task<OperationResult> RemoveUserRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            IdentityResult result =  IdentityResult.Success;

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

	    public async Task<LoginHistoryResult> AddLoginHistory(LoginHistory logHistory, string userId)
	    {
			var result =  this._logHistory.Add(new LoginHistory
		    {
			    Id = Guid.NewGuid(),
				DateTime = logHistory.DateTime,
				IpAddress = logHistory.IpAddress,
				Browser = logHistory.Browser,
				UserId = userId,
				Geolocation = logHistory.Geolocation
			});
		    return new LoginHistoryResult()
		    {
			    DateTime =  result.DateTime,
				Browser = result.Browser,
				IpAddress = result.IpAddress,
				Geolocation = result.Geolocation,
				Screen = result.Screen
		    };

	    }

	    public IQueryable<LoginHistoryResult> GetLoginHistory(StandardFiltersDto filters, string userId, out int totalItems)
		{
			IQueryable<LoginHistory> data = _logHistory.Data;

			filters.PageNumber = filters.PageNumber ?? 0;

			totalItems = data.Count();

			if (!string.IsNullOrWhiteSpace(filters.OrderByField) && orderBys.ContainsKey(filters.OrderByField))
			{
				var orderExp = orderBys[filters.OrderByField];

				data = filters.IsDesc
					? data.OrderByDescending(orderExp)
					: data.OrderBy(orderExp);
			}

			data = filters.PageSize == null
				? data
				: data.Skip((int)(filters.PageNumber)
				            * (int)filters.PageSize)
					.Take((int)filters.PageSize);

			return data.Select(u => new LoginHistoryResult()
			{
				DateTime = u.DateTime,
				IpAddress = u.IpAddress,
				Browser = u.Browser,
				Screen = u.Screen,
				Geolocation = u.Geolocation
			});

	    }
	}
}
