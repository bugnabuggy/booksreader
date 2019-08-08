using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Validators
{
    public class ItemExistsValidator : IBrValidator
    {
        public IActionResult Validate(object item, Guid? id, ClaimsPrincipal user = null)
        {
            var result = item == null
                ? new NotFoundObjectResult(id)
                : null;

            return result;
        }

        public IServiceProvider ServiceProvider { get; set; }
    }
}