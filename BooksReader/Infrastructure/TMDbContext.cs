using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using BooksReader.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace BooksReader.Infrastructure
{
    public class TMDbContext : IdentityDbContext<TMUser>
    {
        public TMDbContext(DbContextOptions<TMDbContext> options)
            : base(options)
        {
        }
    }
}
