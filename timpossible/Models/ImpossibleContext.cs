using Microsoft.EntityFrameworkCore;

namespace timpossible.Models
{
    public class ImpossibleContext : DbContext
    {
        public ImpossibleContext(DbContextOptions<ImpossibleContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}