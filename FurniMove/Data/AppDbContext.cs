using FurniMove.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FurniMove.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },new IdentityRole
                {
                    Id = "3",
                    Name = "ServiceProvider",
                    NormalizedName = "SERVICEPROVIDER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<Appliance>()
            .Property(e => e.Tags)
            .HasConversion(
                v => string.Join(',', v), // Convert list to string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()); // Convert string to list

        }
        public DbSet<MoveRequest> MoveRequests { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Appliance> Appliances { get; set; }
        public DbSet<MoveOffer> MoveOffers { get; set; }
    }
}
