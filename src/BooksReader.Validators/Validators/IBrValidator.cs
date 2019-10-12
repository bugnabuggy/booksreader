using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Validators.Validators
{
    public interface IBrValidator
    {
        IActionResult Validate(object item, Guid? id, ClaimsPrincipal user = null);
        IServiceProvider ServiceProvider { get; set; }
    }
}
