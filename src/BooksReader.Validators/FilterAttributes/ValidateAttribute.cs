using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Validators.Getters;
using BooksReader.Validators.Validators;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BooksReader.Validators.FilterAttributes
{
    /// <summary>
    /// Wanna make your life little bit complicated :) ?
    /// no problem ↓
    /// </summary>
    public class ValidateAttribute : ActionFilterAttribute
    {
        private readonly string _fieldId;
        private readonly Type _getterType;
        private readonly IEnumerable<Type> _validators; // where type is IBrValidator<IValidatable>

        public ValidateAttribute(Type getter, Type[] validators, string fieldId = "id")
        {
            _fieldId = fieldId;
            _getterType = getter;
            _validators = validators;
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            // if something goes wrong, do not process the exception

            var id = (Guid)ctx.ActionArguments[_fieldId];
            var getter = Activator.CreateInstance(_getterType) as IGetter;
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
