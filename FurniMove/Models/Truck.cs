using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurniMove.Models
{
    public class Truck
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string plateNumber { get; set; }
        [Required]
        public string brand { get; set; }
        [Required]
        public string model { get; set; }
        [Required]
        public int year { get; set; }
        [Required]
        public int capacity { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public double consumptionRate { get; set; }
        public Location? currentLocation { get; set; }
        [ForeignKey("currentLocation")]
        public int? currentLocationId { get; set; }
    }
}
