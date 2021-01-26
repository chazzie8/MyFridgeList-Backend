using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Entities;

namespace MyFridgeListWebapi.Core.Data.Database
{
    public sealed class DatabaseContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<Shoppinglist> Shoppinglists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}