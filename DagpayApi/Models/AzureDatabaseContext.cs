using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DagpayApi.Models
{
    public class AzureDatabaseContext : IdentityDbContext
    {
        public AzureDatabaseContext(DbContextOptions<AzureDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
