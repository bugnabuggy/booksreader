using BooksReader.Web.IdentityServerExtensions.Entities;

namespace BooksReader.Web.IdentityServerExtensions.Interfaces
{
    public interface IGoogleAuthProvider:IExternalAuthProvider
    {
        Provider Provider { get; }
    }
}
