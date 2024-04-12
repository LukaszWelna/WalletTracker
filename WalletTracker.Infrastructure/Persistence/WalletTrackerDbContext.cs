﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Infrastructure.Persistence
{
    // DbContext configuration
    public class WalletTrackerDbContext : IdentityDbContext
    {
        public WalletTrackerDbContext(DbContextOptions<WalletTrackerDbContext> options) : base(options)
        {
            
        }

        // Configure relationships and model properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<IncomeCategoriesDefault>(eb =>
            {
                eb.Property(i => i.Name)
                .HasColumnType("varchar(50)");
            });
            
            modelBuilder.Entity<IncomeCategoriesAssignedToUsers>(eb =>
            {
                eb.HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId);

                eb.Property(i => i.Name)
                .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Incomes>(eb =>
            {
                eb.HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);

                eb.HasOne(i => i.Category)
                .WithMany(c => c.Incomes)
                .HasForeignKey(i => i.CategoryId);

                eb.Property(i => i.Amount)
                .HasPrecision(10, 2);

                eb.Property(i => i.Comment)
                .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<ExpenseCategoriesDefault>(eb =>
            {
                eb.Property(e => e.Name)
                .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ExpenseCategoriesAssignedToUsers>(eb =>
            {
                eb.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);

                eb.Property(e => e.Name)
                .HasColumnType("varchar(50)");

                eb.Property(e => e.Limit)
                .HasPrecision(10, 2);
            });

            modelBuilder.Entity<Expenses>(eb =>
            {
                eb.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

                eb.HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId);

                eb.HasOne(e => e.Payment)
                .WithMany(p => p.Expenses)
                .HasForeignKey(e => e.PaymentId);

                eb.Property(e => e.Amount)
                .HasPrecision(10, 2);

                eb.Property(e => e.Comment)
                .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<PaymentMethodsDefault>(eb =>
            {
                eb.Property(p => p.Name)
                .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<PaymentMethodsAssignedToUsers>(eb =>
            {
                eb.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

                eb.Property(p => p.Name)
                .HasColumnType("varchar(50)");
            });
        }
    }
}
