using System;
using Microsoft.EntityFrameworkCore;

namespace SchoolSOA.Services.Blog.Entities
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasData(
                new Blog { Id = Guid.NewGuid(), CreatorId = Guid.NewGuid(), Title = "Title-1", Content = "Content-1" },
                new Blog { Id = Guid.NewGuid(), CreatorId = Guid.NewGuid(), Title = "Title-2", Content = "Content-2" },
                new Blog { Id = Guid.NewGuid(), CreatorId = Guid.NewGuid(), Title = "Title-3", Content = "Content-3" });
        }
    }
}