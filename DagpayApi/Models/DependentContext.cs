using System;
using Microsoft.EntityFrameworkCore;

namespace DagpayApi.Models
{
    public class DependentContext : DbContext
    {
        public DependentContext(DbContextOptions<DependentContext> options) : base(options)
        {
        }

        public DbSet<Dependent> Dependents { get; set; }
    }
}
