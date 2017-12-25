using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskImpossible.Models;

namespace TaskImpossible.Data
{
    public class ImpossibleContext : IdentityDbContext<ApplicationUser>
    {
        public ImpossibleContext(DbContextOptions<ImpossibleContext> options)
            : base(options)
        {
        }

        public DbSet<iTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}