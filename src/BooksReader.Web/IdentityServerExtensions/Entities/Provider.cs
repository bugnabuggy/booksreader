namespace BooksReader.Web.IdentityServerExtensions.Entities
{
    public class Provider
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string UserInfoEndPoint { get; set; }
    }
}
