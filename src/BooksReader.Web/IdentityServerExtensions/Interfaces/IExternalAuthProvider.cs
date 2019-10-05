using Newtonsoft.Json.Linq;

namespace BooksReader.Web.IdentityServerExtensions.Interfaces
{
    public interface IExternalAuthProvider
    {
        JObject GetUserInfo(string accessToken);
    }
}
