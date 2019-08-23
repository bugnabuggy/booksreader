using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using BooksReader.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class PersonalPageService : CRUDService<PersonalPage>, IPersonalPageService, IValidator<PersonalPage>
    {
        

        private readonly IEnumerable<Expression<Func<PersonalPage, string>>> _validations =
            new List<Expression<Func<PersonalPage, string>>>()
            {
                {
                    x => string.IsNullOrWhiteSpace(x.Domain)
                        ? MessageStrings.PersonalPageMessages.DomainCantBeEmpty
                        : ""
                }
            };


        public PersonalPageService(IRepository<PersonalPage> repo) : base(repo)
        {
        }

        public IEnumerable<string> Validate(PersonalPage page)
        {


            throw new NotImplementedException();
        }


        public override async Task<IOperationResult<PersonalPage>> AddAsync(PersonalPage item)
        {
            var result = new OperationResult<PersonalPage>(item);

            var validations = Validate(item).ToList();
            if (validations.Any())
            {
                result.Messages = validations;
            } else
            {
                await Repository.AddAsync(item);
                result.Success = true;
            }

            return result;
        }

        
    }
}
