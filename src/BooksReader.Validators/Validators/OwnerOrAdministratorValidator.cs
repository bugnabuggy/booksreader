using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BooksReader.Validators.Validators
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
