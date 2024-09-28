using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracticeSite.Data.EntityConfigurations;
using PracticeSite.Data.Identity;
using PracticeSite.Models.Entities;

namespace PracticeSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationForm> Applications { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationFormConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
