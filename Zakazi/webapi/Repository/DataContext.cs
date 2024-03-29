﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Diagnostics;
using webapi.Domain.Models;
using Microsoft.AspNetCore.Identity;


namespace webapi.Repository
{
    public class DataContext : IdentityDbContext<ZakaziUser, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
        IdentityUserToken<int> >
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ZakaziUser>(entity =>
            {
                entity.HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            });

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
