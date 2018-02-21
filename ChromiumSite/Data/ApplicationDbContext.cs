using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChromiumSite.Models;

namespace ChromiumSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AquaImage> AquaImages { get; set; }
        public DbSet<AquaProposalModel> AquaProposalModels { get; set; }
        public DbSet<ChromeProposalModel> ChromeProposalModels { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AquaProposalModel>().Property(x => x.Created_at).HasDefaultValueSql("getdate()");
            builder.Entity<ChromeProposalModel>().Property(x => x.Created_at).HasDefaultValueSql("getdate()");

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ChromiumSite.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
