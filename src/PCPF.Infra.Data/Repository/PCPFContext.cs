using Microsoft.EntityFrameworkCore;

namespace PCPF.Infra.Data.Repository
{
    public class PCPFContext:DbContext
    {
        public PCPFContext(DbContextOptions<PCPFContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PCPFContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
