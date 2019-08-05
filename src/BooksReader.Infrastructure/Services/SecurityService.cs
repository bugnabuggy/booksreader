using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;

namespace BooksReader.Infrastructure.Services
{
	public class SecurityService: ISecurityService
	{
		private readonly UserManager<BrUser> _userManager;

		public SecurityService(
			UserManager<BrUser> userManager
			)
		{
			_userManager = userManager;
		}

        public bool IsInRole(Guid userId, string role)
        {
            var user = _userManager.FindByIdAsync(userId.ToString()).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
            return roles.Contains(role);
        }

        /// <summary>
        /// Check if user is admin
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
		public bool HasAccess(Guid userId)
		{
			try
			{
				var user = _userManager.FindByIdAsync(userId.ToString()).Result;
				return _userManager.IsInRoleAsync(user,SiteRoles.Admin).Result;
			}
			catch (Exception exp)
			{
				return false;
			}

		}

        /// <summary>
        /// Check if user is the owner of the item
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
		public bool HasAccess(Guid userId, IOwned item)
		{
			var result = item.OwnerId.Equals(userId);
			if (result)
			{
				return true;
			}

			var isAdmin = HasAccess(userId);
			return isAdmin;
		}

        public bool HasAccess(ClaimsPrincipal principal, IOwned item)
        {
            try
            {
                var user = _userManager.GetUserAsync(principal).Result;
                var result = item.OwnerId.Equals(user.Id);
                if (result)
                {
                    return true;
                }

                var isAdmin = _userManager.IsInRoleAsync(user, SiteRoles.Admin).Result;
                return isAdmin;
            }
            catch (Exception exp)
            {
                return false;
            }
            

        }

        public bool HasAccess(Guid userId, SecurityAction action)
		{
			throw new NotImplementedException();
		}
	}
}
