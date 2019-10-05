using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Infrastructure.DataContext
{
    public class BrDbContext : IdentityDbContext<BrUser, IdentityRole<Guid>, Guid>
    {
        public BrDbContext(DbContextOptions<BrDbContext> options)
            : base(options)
        {
        }

    }
}
