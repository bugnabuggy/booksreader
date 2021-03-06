﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Services
{
    public interface ISecurityService
    {
        bool HasAccess(Guid userId);
        bool HasAccess(Guid userId, IOwned item);
        bool HasAccess(ClaimsPrincipal user, IOwned item);
        bool IsInRole(Guid userId, string role);
    }
}
