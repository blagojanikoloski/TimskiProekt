using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Diagnostics;
using webapi.Domain.Models;
using Microsoft.AspNetCore.Identity;


namespace webapi.Repository
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ZakaziUser> ZakaziUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.AvailabilityFrom)
                    .HasColumnType("datetimeoffset")
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.Property(e => e.AvailabilityTo)
                    .HasColumnType("datetimeoffset")
                    .HasDefaultValueSql("SYSUTCDATETIME()");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetimeoffset")
                    .HasDefaultValueSql("SYSUTCDATETIME()");
            });
        }
    }
}
