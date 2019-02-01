﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;

namespace BooksReader.Core.Services
{
    public interface IUsersService
    {
        IQueryable<UserResult> GetUsersWithRoles();

        Task<OperationResult> AddUserRole(string username, string role);
        Task<OperationResult> RemoveUserRole(string username, string role);
	    Task<OperationResult> ToggleUserRole(string username, string role);
	    Task<LoginHistoryResult> AddLoginHistory(LoginHistory logHistory, string userId);
	    IQueryable<LoginHistoryResult> GetLoginHistory(StandardFiltersDto filters, string userId, out int totalItems);
    }
}