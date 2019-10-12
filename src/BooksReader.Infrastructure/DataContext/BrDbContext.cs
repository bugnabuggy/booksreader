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

        public DbSet<TypesList> TypesLists { get; set; }
        public DbSet<TypeValue> TypeValues { get; set; }

        public DbSet<LoginHistory> LoginHistory { get; set; }
        public DbSet<AuthorProfile> AuthorProfiles { get; set; }

        public DbSet<UserDomain> UserDomains{ get; set; }
        public DbSet<PublicPage> PublicPages { get; set; }
        public DbSet<SeoInfo> SeoInfos{ get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
