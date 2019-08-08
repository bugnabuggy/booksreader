using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Validators
{
    public interface IBrValidator
    {
        IActionResult Validate(object item, Guid? id, ClaimsPrincipal user = null);
        IServiceProvider ServiceProvider { get; set; }
    }
}