using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.DTO;
using BooksReader.Infrastructure.SeedData;
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

        public DbSet<TypesList> TypesLists { get; set; }
        public DbSet<TypeValue> TypeValues { get; set; }

        public DbSet<SeoInfo> SeoInfos { get; set; }
        public DbSet<PersonalPage> PersonalPages { get; set; }
        public DbSet<AuthorProfile> AuthorProfiles { get; set; }
	    public DbSet<Book> Books { get; set; }
	    public DbSet<BookChapter> BookChapters { get; set; }
        public DbSet<BookChapterHistory> BookChaptersHistory { get; set; }
        public DbSet<BookPrice> BooksPrices { get; set; }
        public DbSet<LoginHistory> LoginHistory { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Indexes
            builder.Entity<Book>()
                .HasIndex(x => x.Title);
            builder.Entity<Book>()
                .HasIndex(x => x.Author);
            builder.Entity<Book>()
                .HasIndex(x => x.OwnerId);
            builder.Entity<Book>()
                .HasIndex(x => x.Created);
            builder.Entity<Book>()
                .HasIndex(x => x.Published);

            builder.Entity<BookChapter>()
                .HasIndex(x => x.Created);
            builder.Entity<BookChapter>()
                .HasIndex(x => x.OwnerId);
            builder.Entity<BookChapter>()
                .HasIndex(x => x.Title);
            builder.Entity<BookChapter>()
                .HasIndex(x => x.Version);
            builder.Entity<BookChapter>()
                .HasIndex(x => x.Number);

            builder.Entity<BookChapterHistory>()
                .HasIndex(x => x.Date);
            builder.Entity<BookChapterHistory>()
                .HasIndex(x => x.OwnerId);
                

            builder.Entity<AuthorProfile>()
                .HasIndex(x => x.AuthorName);

            builder.Entity<PersonalPage>()
                .HasIndex(x => x.Domain);
            builder.Entity<PersonalPage>()
                .HasIndex(x => x.SubjectId);
        }
    }
}
