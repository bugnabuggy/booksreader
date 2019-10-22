using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.DTO.Admin;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Models.Requests.Admin;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using BooksReader.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class DomainsService : IDomainsService
    {
        private readonly IRepository<UserDomain> _domainsRepo;
        private readonly IRepository<BrUser> _usersRepo;
        private readonly IRepository<PublicPage> _pagesRepo;
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
            IRepository<UserDomain> domainsRepo,
            IRepository<BrUser> usersRepo,
            IRepository<PublicPage> pagesRepo
            )
        {
            _domainsRepo = domainsRepo;
            _mapper = mapper;
            _security = security;
            _usersRepo = usersRepo;
            _pagesRepo = pagesRepo;
        }

        public IWebResult<IEnumerable<UserDomainStateDto>> Get(AllDomainsFilters filters)
        {
            var data = _domainsRepo.Data.AsNoTracking();

            
            data = string.IsNullOrEmpty(filters.Name)
                ? data
                : data.Where(x => x.Name.Equals(filters.Name, StringComparison.CurrentCultureIgnoreCase));

            var userIds = _usersRepo.Data.AsNoTracking()
                .Where(x => x.UserName.Equals(filters.Username, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);

            data = string.IsNullOrWhiteSpace(filters.Username)
                ? data
                : data.Where(x => userIds.Contains(x.OwnerId));

            data = PaginationHelper.GetPaged(data, filters, out int totalRecords);

            var domainsStats = data.Join(_usersRepo.Data,
                d => d.OwnerId,
                u => u.Id,
                (d, u) => new UserDomainStateDto()
                {
                    Type = d.VerificationType,
                    Username = u.UserName,
                    DomainId = d.Id,
                    DomainName = d.Name,
                    Protocol = d.Protocol,
                    Verified = d.Verified,
                    VerificationDate = d.VerificationDate,
                    VerificationRequested = d.VerificationRequested,
                    NumberOfPages =  _pagesRepo.Data.AsNoTracking().Count(x=>x.DomainId.Equals(d.Id))
                });

            var result = new WebResult<IEnumerable<UserDomainStateDto>>()
            {
                PageSize = filters.PageSize,
                PageNumber = filters.PageNumber,
                Total = totalRecords,
                Data = domainsStats,
                Success = true,
            };

            return result;
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
            if (!entity.OwnerId.Equals(domain.OwnerId))
            {
                if (_security.IsInRole(actingUser.Id, SiteRoles.Admin))
                {
                    entity.OwnerId = domain.OwnerId;
                }
                else
                {
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

        public IOperationResult<UserDomainDto> ToggleVerification(Guid domainId)
        {
            var domain = _domainsRepo.Data
                .FirstOrDefault(x => x.Id.Equals(domainId));

            // cancel verification for other domains with the same name
            var otherDomainsWithSameName = _domainsRepo.Data.Where(x =>
                x.Name.Equals(domain.Name, StringComparison.InvariantCultureIgnoreCase) 
                && !x.Id.Equals(domainId));

            otherDomainsWithSameName.ForEachAsync(x => x.Verified = false).Wait();

            domain.Verified = !domain.Verified;

            _domainsRepo.Update(otherDomainsWithSameName);
            _domainsRepo.Update(domain);

            var result = new OperationResult<UserDomainDto>(true);
            result.Data = _mapper.Map<UserDomainDto>(domain);
            return result;

        }

        IEnumerable<string> Validate(UserDomainRequest domain, BrUser actingUser)
        {
            var errors = _validations
                .Select(validation => validation(domain, this, actingUser))
                .ToList();

            return errors.Where(x => !(x == null || x.Trim() == string.Empty));
        }
    }
}
