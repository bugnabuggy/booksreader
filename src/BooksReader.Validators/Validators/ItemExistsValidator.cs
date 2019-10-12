using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Validators.Validators
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
