using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : DbContext(options)
    {
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .OwnsOne(r => r.Address);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Dished)
                .WithOne()
                .HasForeignKey(d => d.RestaurantId);
            
        }

    }
}
