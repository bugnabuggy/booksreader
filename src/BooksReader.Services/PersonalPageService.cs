using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using BooksReader.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class PersonalPageService : CRUDService<PersonalPage>, IPersonalPageService, IValidator<PersonalPage>
    {
        

        private readonly IEnumerable<Func<PersonalPage, PersonalPageService, string>> _validations =
            new List<Func<PersonalPage, PersonalPageService, string>>()
            {
                {
                    (x, svc) => string.IsNullOrWhiteSpace(x.Domain)
                        ? MessageStrings.PersonalPageMessages.DomainCantBeEmpty
                        : ""
                }
            };


        public PersonalPageService(IRepository<PersonalPage> repo) : base(repo)
        {
        }


        public IEnumerable<string> Validate(PersonalPage page)
        {
            var messages = new List<string>();

            foreach (var validation in _validations)
            {
                var msg = validation.Invoke(page, this);
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    messages.Add(msg);
                }
            }

            return messages;
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


        public IOperationResult<PersonalPage> Save(PublicPageRequest request, Guid subjectId, PersonalPageType type)
        {
            var result = new OperationResult<PersonalPage>(true);

            var existing = Repository.Data.Where(x => x.SubjectId.HasValue)
                .Where(x => x.SubjectId.Equals(subjectId))
                .FirstOrDefault(x => x.PageType == type);

            if (existing == null)
            {
                var newPage = new PersonalPage()
                {
                    Content = request.Content,
                    Domain = request.Domain,
                    UrlPath = request.UrlPath,
                    PageType = type,
                    SubjectId = subjectId
                };

                Repository.Add(newPage);
                result.Data = newPage;

            }

            existing.Content = request.Content;
            existing.UrlPath = request.UrlPath;
            existing.Domain = request.Domain;

            Repository.Update(existing);
            result.Data = existing;

            return result;
        }
    }
}
