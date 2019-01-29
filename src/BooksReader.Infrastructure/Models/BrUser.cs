using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BooksReader.Infrastructure.Models
{
    public class BrUser : IdentityUser
    {
	    public BrUser() : base()
	    {
	    }
        public string Name { get; set; }
    }
}
