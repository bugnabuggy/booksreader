using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
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

    public class Getter <T> : IGetter where T: IIdentified {
      
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

    public interface IValidatable : IOwned, IIdentified
    {
    }

    public interface IBrValidator<IValidatable>
    {
        IActionResult Validate(IValidatable item, Guid id);
    }

    public class ItemExistsValidator<IValidatable> : IBrValidator<IValidatable> 
    {
        public IActionResult Validate(IValidatable item, Guid id)
        {

            var result = item == null
                ? new NotFoundObjectResult(id)
                 : null;

            return result;
        }
    }


    /// <summary>
    /// Wanna make your life little bit complicated :) ?
    /// no problem ↓
    /// </summary>
    public class ValidateAttribute: ActionFilterAttribute
    {
        private readonly string _fieldId;
        private readonly Type _type;
        private readonly IEnumerable<Type> _validators;

        public ValidateAttribute(Type getter, IEnumerable<Type> validators, string fieldId = "id")
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

            var security = services.GetRequiredService<ISecurityService>();

            var item = getter.Get(id, services) as IValidatable;

            foreach (var type in _validators)
            {
                var validator = Activator.CreateInstance(type) as IBrValidator<IValidatable>;
                var result = validator.Validate(item, id);
                if (result != null)
                {
                    ctx.Result = result;
                    return;
                }
            }

            if(!security.HasAccess(ctx.HttpContext.User, item))
            {
                ctx.Result = new ForbidResult();
                return;
            }
        }
    }
}
