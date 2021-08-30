using EUROMONITOR.Model.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EUROMONITOR.Model.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookConfiguration());

            modelBuilder.Entity<Subscription>()
                .HasKey(bc => new { bc.Id, bc.BookId });

            modelBuilder.Entity<Subscription>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Subscriptions)
                .HasForeignKey(bc => bc.Id);

            modelBuilder.Entity<Subscription>()
                .HasOne(bc => bc.Book)
                .WithMany(c => c.Subscriptions)
                .HasForeignKey(bc => bc.BookId);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}
