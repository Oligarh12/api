using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}