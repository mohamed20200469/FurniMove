using FurniMove.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },new IdentityRole
                {
                    Name = "ServiceProvider",
                    NormalizedName = "SERVICEPROVIDER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
        public DbSet<MoveRequest> MoveRequests { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Appliance> Appliances { get; set; }
        public DbSet<MoveOffer> MoveOffers { get; set; }
    }
}
