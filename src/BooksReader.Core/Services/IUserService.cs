using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests.User;

namespace BooksReader.Core.Services
{
    public interface IUsersService
    {
        IQueryable<UserResult> GetUsersWithRoles();

        Task<OperationResult> AddUserRole(string username, string role);
        Task<OperationResult> RemoveUserRole(string username, string role);
        Task<OperationResult> ToggleUserRole(string username, string role);

        Task<LoginHistory> AddLoginHistory(LoginHistory logHistory, Guid userId);
        IQueryable<LoginHistory> GetLoginHistory(StandardFilters filters, Guid userId, out int totalItems);

        Task<OperationResult<AppUserDto>> Update(UserProfileRequest data);
        Task<OperationResult<AppUserDto>> Delete(string username);
    }
}
