using System;
using Core;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder
                .Entity<User>()
                .Property(e => e.DateRegistration)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            
            modelBuilder
                .Entity<User>()
                .Property(e => e.DateLastActivity)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            
            modelBuilder.Entity<User>()
                .HasData(
                    new User {Id = 1, DateRegistration = new (2021, 6, 16), DateLastActivity = new (2021, 6, 24)},
                    new User {Id = 2, DateRegistration = new (2021, 6, 17), DateLastActivity = new (2021, 6, 23)},
                    new User {Id = 3, DateRegistration = new (2021, 6, 18), DateLastActivity = new (2021, 6, 26)},
                    new User {Id = 4, DateRegistration = new (2021, 6, 19), DateLastActivity = new (2021, 6, 26)},
                    new User {Id = 5, DateRegistration = new (2021, 6, 20), DateLastActivity = new (2021, 6, 27)}
                );
        }
    }
}