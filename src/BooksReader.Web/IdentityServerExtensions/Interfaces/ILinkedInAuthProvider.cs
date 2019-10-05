using BooksReader.Web.IdentityServerExtensions.Entities;

namespace BooksReader.Web.IdentityServerExtensions.Interfaces
{
   public interface ILinkedInAuthProvider : IExternalAuthProvider
    {
        Provider Provider { get; }
    }
}
