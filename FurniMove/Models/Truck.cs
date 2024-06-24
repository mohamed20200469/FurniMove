using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurniMove.Models
{
    public class Truck
    {
        [Key]
        public int Id { get; set; }
        public AppUser? ServiceProvider { get; set; }
        [ForeignKey("ServiceProvider")]
        public string? ServiceProviderId { get; set; }
        [Required]
        public string? PlateNumber { get; set; }
        [Required]
        public string? Brand { get; set; }
        [Required]
        public string? Model { get; set; }
        [Required]
        public int Year { get; set; }
        public Location? CurrentLocation { get; set; }
        [ForeignKey("currentLocation")]
        public int? CurrentLocationId { get; set; }
        [Required]
        public string? Type { get; set; }
    }
}
