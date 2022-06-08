using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Third_exercise_REMAKE.Core.Models;
using Third_exercise_REMAKE.DAL.Configuration;

namespace Third_exercise_REMAKE.DAL.Infrastructure
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgreementConfiguration());
        }
        public DbSet<AgreementModel> Agreements { get; set; }
    }
}
