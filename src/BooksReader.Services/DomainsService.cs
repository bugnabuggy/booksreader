using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Models.Requests.Admin;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;

namespace BooksReader.Services
{
    public class DomainsService : IDomainsService
    {
        private readonly IRepository<UserDomain> _domainsRepo;
        private readonly ISecurityService _security;
        private readonly IMapper _mapper;

        private IEnumerable<Func<UserDomainRequest, DomainsService, BrUser, string>> _validations = 
            new List<Func<UserDomainRequest, DomainsService, BrUser, string>>()
            {
                // check that domain name is not empty
                (domain, svc, user) =>
                {
                    var msg = string.IsNullOrWhiteSpace(domain.Name)
                        ? MessageStrings.UserDomainsMessages.DomainCantBeEmpty
                        : "";

                    return msg;
                },

                // check if user is owner or admin of domain record
                (domain, svc, user) =>
                {
                    if (domain.OwnerId != Guid.Empty) return "";

                    var hasAccess = svc._security.HasAccess(user.Id, domain);

                    var msg = !hasAccess
                        ? MessageStrings.DoNotHavePermissions
                        : "";

                    return msg;
                }
            };


        public DomainsService(
            IMapper mapper,
            ISecurityService security,
            IRepository<UserDomain> domainsRepo
            )
        {
            _domainsRepo = domainsRepo;
            _mapper = mapper;
            _security = security;
        }

        public IOperationResult<IEnumerable<UserDomain>> Get(AllDomainsFilters filters)
        {
            throw new NotImplementedException();
        }

        public IOperationResult<UserDomainDto> Add(UserDomainRequest domain, BrUser actingUser)
        {
            var result = new OperationResult<UserDomainDto>();
            if (domain == null || actingUser == null)
            {
                result.Messages.Add(MessageStrings.UserDomainsMessages.DataIsNotDefined);
                return result;
            }

            // can only add domain for self or if a user is an admin
            if (domain.OwnerId == Guid.Empty) 
            { 
                domain.OwnerId = actingUser.Id;

                var domainsCount = _domainsRepo.Data.Count(x => x.OwnerId.Equals(actingUser.Id));
                if (domainsCount >= Constants.MaxUserDomainsCount)
                {
                    result.Messages.Add(MessageStrings.UserDomainsMessages.MaxLimitExceed);
                    return result;
                }
            }

            var validations = Validate(domain, actingUser).ToList();

            if (validations.Any())
            {
                result.Messages = validations;
                return result;
            }

            var entity = _mapper.Map<UserDomain>(domain);
            entity.VerificationCode = Guid.NewGuid();

            entity = _domainsRepo.Add(entity);
            result.Data = _mapper.Map<UserDomainDto>(entity);
            result.Success = true;

            return result;
        }

        public IOperationResult<UserDomainDto> Update(UserDomainRequest domain, BrUser actingUser)
        {
            var result = new OperationResult<UserDomainDto>();

            var validations = Validate(domain, actingUser).ToList();

            if (validations.Any())
            {
                result.Messages = validations;
                return result;
            }

            var entity = _domainsRepo.Data.FirstOrDefault(x => x.Id.Equals(domain.Id));

            // if name changed verification is obsolete
            if (!domain.Name.Equals(entity.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                entity.Verified = false;
            }

            entity.Name = domain.Name;
            entity.Protocol = domain.Protocol;
            entity.VerificationType = domain.VerificationType;

            // id certificate changed need to install new one
            if (!string.IsNullOrWhiteSpace(domain.Certificate)
                && !domain.Certificate.Equals(entity.Certificate))
            {
                entity.Certificate = domain.Certificate;
                // do some staff with certificate installation?
            }

            // can update owner only if user is administrator
            if (!entity.OwnerId.Equals(domain.OwnerId) )
            {
                if (_security.IsInRole(actingUser.Id, SiteRoles.Admin))
                {
                    entity.OwnerId = domain.OwnerId;
                } else { 
                    result.Messages.Add(MessageStrings.UserDomainsMessages.CantChangeAnOwner);      
                }
            }

            if (result.Messages.Any())
            {
                return result;
            }

            _domainsRepo.Update(entity);
            result.Success = true;
            result.Data = _mapper.Map<UserDomainDto>(entity);

            return result;
        }


        public IOperationResult<UserDomainDto> Delete(Guid id, BrUser actingUser)
        {
            var result = new OperationResult<UserDomainDto>();
            var entity = _domainsRepo.Data.FirstOrDefault(x => x.Id.Equals(id));

            _domainsRepo.Delete(entity);

            result.Data = _mapper.Map<UserDomainDto>(entity);
            result.Success = true;
            return result;
        }

        IEnumerable<string> Validate(UserDomainRequest domain, BrUser actingUser)
        {
            var errors = _validations
                .Select(validation => validation(domain, this, actingUser))
                .ToList();

            return errors.Where(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}
