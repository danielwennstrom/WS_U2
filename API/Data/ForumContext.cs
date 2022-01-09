using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Topic> Topics { get; set; } = null!;
    }
}