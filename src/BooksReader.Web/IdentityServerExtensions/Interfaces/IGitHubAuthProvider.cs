using BooksReader.Web.IdentityServerExtensions.Entities;

namespace BooksReader.Web.IdentityServerExtensions.Interfaces
{
    public interface IGitHubAuthProvider : IExternalAuthProvider
    {
        Provider Provider { get; }
    }
}
