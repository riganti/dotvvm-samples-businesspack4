using BPSamples.GridViewExporting.Data;
using Microsoft.EntityFrameworkCore;

namespace BPSamples.GridViewExporting
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=LocalDatabase.db;", b => b.MigrationsAssembly("BPSamples.GridViewExporting"));
            }
        }

        public virtual DbSet<TimeTrackingEntry> TimeTrackingEntries { get; set; }


    }
}
