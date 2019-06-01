using System;
using Microsoft.EntityFrameworkCore;

namespace DagpayApi.Models
{
    public class AzureDatabaseContext : DbContext
    {
        public AzureDatabaseContext(DbContextOptions<AzureDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
    }
}
