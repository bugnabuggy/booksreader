using BooksReader.Web.IdentityServerExtensions.Entities;

namespace BooksReader.Web.IdentityServerExtensions.Interfaces
{
    public interface IFacebookAuthProvider : IExternalAuthProvider
    {
        Provider Provider { get; }
    }
}
