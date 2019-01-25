using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using BooksReader.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace BooksReader.Web.Infrastructure
{
    public class BrDbContext : IdentityDbContext<BrUser>
    {
        public BrDbContext(DbContextOptions<BrDbContext> options)
            : base(options)
        {
        }
    }
}
