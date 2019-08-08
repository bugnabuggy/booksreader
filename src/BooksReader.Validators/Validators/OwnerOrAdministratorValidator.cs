using System;
using System.Security.Claims;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BooksReader.Validators
{
    public class OwnerOrAdministratorValidator : IBrValidator
    {
        public IActionResult Validate(object item, Guid? id, ClaimsPrincipal user = null)
        {
            var security = ServiceProvider.GetRequiredService<ISecurityService>();
            var hasAccess = security.HasAccess(user, item as IOwned);

            var result = hasAccess
                ? null
                : new ForbidResult(MessageStrings.DoNotHavePermissions);

            return result;
        }

        public IServiceProvider ServiceProvider { get; set; }
    }
}