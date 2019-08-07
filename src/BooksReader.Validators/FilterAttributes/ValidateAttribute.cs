using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Infrastructure.Repositories;
using BooksReader.Validators.FilterAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;


namespace BooksReader.Validators.FilterAttributes
{

    public interface IGetter
    {
        object Get(Guid id, IServiceProvider provider);
    }

    public class Getter<T> : IGetter where T : IIdentified
    {

        public Getter()
        {
        }

        public object Get(Guid id, IServiceProvider provider)
        {
            var repo = provider.GetRequiredService<IRepository<T>>();
            var item = repo.Data.FirstOrDefault(x => x.Id.Equals(id));
            return item;
        }
    }

    public interface IBrValidator
    {
        IActionResult Validate(object item, Guid? id, ClaimsPrincipal user = null);
        IServiceProvider ServiceProvider { get; set; }
    }

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

    public class OwnerOrAdministratorValidator : IBrValidator
    {
        public IActionResult Validate(object item, Guid? id, ClaimsPrincipal user = null)
        {
            var security = ServiceProvider.GetRequiredService<ISecurityService>();
            var hasAccess = security.HasAccess(user, item as IOwned);

            var result = hasAccess
                         ? null
                         : new ForbidResult();

            return result;
        }

        public IServiceProvider ServiceProvider { get; set; }
    }


    /// <summary>
    /// Wanna make your life little bit complicated :) ?
    /// no problem ↓
    /// </summary>
    public class ValidateAttribute : ActionFilterAttribute
    {
        private readonly string _fieldId;
        private readonly Type _type;
        private readonly IEnumerable<Type> _validators; // where type is IBrValidator<IValidatable>

        public ValidateAttribute(Type getter, Type[] validators, string fieldId = "id")
        {
            _fieldId = fieldId;
            _type = getter;
            _validators = validators;
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            // if something goes wrong, do not process the exception

            var id = (Guid)ctx.ActionArguments[_fieldId];
            var getter = Activator.CreateInstance(_type) as IGetter;
            var services = ctx.HttpContext.RequestServices;

            var item = getter.Get(id, services);

            foreach (var type in _validators)
            {
                var validator = Activator.CreateInstance(type) as IBrValidator;
                validator.ServiceProvider = services;

                var result = validator.Validate(item, id, ctx.HttpContext.User);
                if (result != null)
                {
                    ctx.Result = result;
                    return;
                }
            }
        }
    }
}
