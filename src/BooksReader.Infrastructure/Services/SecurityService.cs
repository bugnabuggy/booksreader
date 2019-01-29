using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.Models;
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

		public bool HasAccess(Guid userId)
		{
			try
			{
				var user = _userManager.FindByIdAsync(userId.ToString()).Result;
				var roles = _userManager.GetRolesAsync(user).Result;
				return roles.Contains(SiteRoles.Admin);
			}
			catch (Exception exp)
			{
				return false;
			}

		}

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

		public bool HasAccess(Guid userId, SecurityAction action)
		{
			throw new NotImplementedException();
		}
	}
}
