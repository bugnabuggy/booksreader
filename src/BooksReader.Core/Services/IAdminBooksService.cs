using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Admin;
using BooksReader.Core.Models.Requests.Admin;

namespace BooksReader.Core.Services
{
    public interface IAdminBooksService
    {
        IWebResult<IEnumerable<AdminBookDto>> GetBooks(AdminAllBooksFilter filters);
        IOperationResult<AdminBookDto> ChangeVerification(AdminBookVerificationRequest request);
    }
}
