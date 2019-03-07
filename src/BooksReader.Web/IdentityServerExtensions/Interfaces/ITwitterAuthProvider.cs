using BooksReader.Web.IdentityServerExtensions.Entities;

namespace BooksReader.Web.IdentityServerExtensions.Interfaces
{
    public interface ITwitterAuthProvider : IExternalAuthProvider
    {
        Provider Provider { get; }
    }
}
