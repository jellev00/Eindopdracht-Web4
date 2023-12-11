using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.EF.Models
{
    public class RestaurantContext : DbContext
    {
        private string _connectionString;

        public RestaurantContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<GebruikerEF> Gebruiker { get; set; }
        public DbSet<ReservatiesEF> Reservatie { get; set; }
        public DbSet<RestaurantEF> Restaurant { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GebruikerEF>()
                .HasMany(g => g.Reservaties)
                .WithOne(r => r.Gebruiker);

            modelBuilder.Entity<RestaurantEF>()
                .HasMany(r => r.Reservaties)
                .WithOne(r => r.Restaurant);
        }
    }
}
