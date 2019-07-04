using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BooksReader.Core.Entities
{
    public class BrUser : IdentityUser
    {
	    public BrUser() : base()
	    {
	    }
        [MaxLength(250)]
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
