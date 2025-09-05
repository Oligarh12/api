using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DBContext : IdentityDbContext<AppUser>
    {
        public DBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<DiscussionMessage> DiscussionMessages { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Seed roles
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

            // Relationships and delete behaviors (soft delete -> restrict cascade paths)
            builder.Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasOne(o => o.Company)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Discussion>()
                .HasOne(d => d.Company)
                .WithMany(c => c.Discussions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Discussion>()
                .HasOne(d => d.Order)
                .WithOne(o => o.Discussion)
                .HasForeignKey<Discussion>(d => d.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Document>()
                .HasOne(doc => doc.Discussion)
                .WithMany(d => d.Documents)
                .HasForeignKey(doc => doc.DiscussionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DiscussionMessage>()
                .HasOne(m => m.Discussion)
                .WithMany(d => d.Messages)
                .HasForeignKey(m => m.DiscussionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Global query filters for soft-deleted entities
            builder.Entity<Company>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Employee>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Order>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Discussion>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<DiscussionMessage>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Document>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
