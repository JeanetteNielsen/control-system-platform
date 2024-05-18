using ControlSystemPlatform.DAL.Enities;
using Microsoft.EntityFrameworkCore;
using ControlSystemPlatform.Shared;

namespace ControlSystemPlatform.DAL
{
    public class WarehouseDbContext(DbContextOptions<WarehouseDbContext> options, IScopedContext scopedContext)
        : DbContext(options)
    {
        public DbSet<TransportOrderEntity> TransportOrder { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(b => b.Weight).HasPrecision(8, 3);


            modelBuilder.Entity<TransportOrderEntity>()
                .HasIndex(item => item.RequesterOrderReference)
                .IsUnique();

            modelBuilder
                .Entity<TransportOrderEntity>()
                .Property(d => d.Priority)
                .HasConversion<string>();

            modelBuilder
                .Entity<TransportOrderEntity>()
                .Property(d => d.Status)
                .HasConversion<string>();

            modelBuilder
                .Entity<TransportOrderEntity>()
                .Property(d => d.HandlingStatus)
                .HasConversion<string>();


            modelBuilder.Entity<TransportOrderEntity>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");


            modelBuilder.Entity<Item>(entity =>
            {
                entity.OwnsOne(e => e.Dimensions, dimensions =>
                {
                    dimensions.Property(d => d.Height).HasColumnName("Height").HasPrecision(8, 2);
                    dimensions.Property(d => d.Length).HasColumnName("Length").HasPrecision(8, 2);
                    dimensions.Property(d => d.Width).HasColumnName("Width").HasPrecision(8, 2);
                });
            });
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditFields();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// TODO: We need a more detailed audit approach for this complexity
        /// </summary>
        private void UpdateAuditFields()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e is { Entity: IUpdateableEntity, State: EntityState.Added or EntityState.Modified });

            foreach (var entry in entries)
            {
                var updateableEntry = (IUpdateableEntity)entry;
                updateableEntry.UpdatedAt = DateTime.UtcNow;
                updateableEntry.UpdatedBy = scopedContext.UserId;
            }
        }
    }
}