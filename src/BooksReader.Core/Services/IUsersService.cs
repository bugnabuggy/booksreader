using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Models.DTO;

namespace BooksReader.Core.Services
{
    public interface IUsersService
    {
        IQueryable<UserResult> GetUsersWithRoles();
    }
}
