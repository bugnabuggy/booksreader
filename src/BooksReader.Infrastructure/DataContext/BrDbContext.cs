using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace BooksReader.Infrastructure.DataContext
{
    public class BrDbContext : IdentityDbContext<BrUser, IdentityRole<Guid>, Guid>
    {
        public BrDbContext(DbContextOptions<BrDbContext> options)
            : base(options)
        {
        }

        public DbSet<SeoInfo> SeoInfos { get; set; }
        public DbSet<PersonalPage> PersonalPages { get; set; }
        public DbSet<AuthorProfile> AuthorProfiles { get; set; }
	    public DbSet<Book> Books { get; set; }
	    public DbSet<BookChapter> BookChapters { get; set; }
	    public DbSet<LoginHistory> LoginHistory { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>()
                .HasIndex(x => x.Title);
            builder.Entity<Book>()
                .HasIndex(x => x.Author);
            builder.Entity<Book>()
                .HasIndex(x => x.Created);
            builder.Entity<Book>()
                .HasIndex(x => x.Published);

            builder.Entity<AuthorProfile>()
                .HasIndex(x => x.AuthorName);

            builder.Entity<PersonalPage>()
                .HasIndex(x => x.Domain);
            builder.Entity<PersonalPage>()
                .HasIndex(x => x.SubjectId);
        }
    }
}
