using Microsoft.EntityFrameworkCore;

namespace Badge.Entities
{
    public class BadgeDbContext : DbContext
    {
        public DbSet<Badge> Badges { get; set; }
        
        public BadgeDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}