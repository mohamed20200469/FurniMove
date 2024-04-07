using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurniMove.Models
{
    public class MoveRequest
    {
        [Key]
        public int Id { get; set; }
        public AppUser? customer { get; set; }
        [ForeignKey("customer")]
        public string? customerId { get; set; }
        public AppUser? serviceProvider { get; set; }
        [ForeignKey("serviceProvider")]
        public string? serviceProviderId { get; set; }
        [Required]
        public required string status { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int? rating { get; set; }
        public double? cost { get; set; }
        public int numOfAppliances { get; set; }
        public ICollection<Appliance> appliances { get; } = new List<Appliance>();
        public Location? startLocation { get; set; }
        public Location? endLocation { get; set;}
        [ForeignKey("startLocation")]
        public int? startLocationId { get; set; }
        [ForeignKey("endLocation")]
        public int? endLocationId { get; set; }
        public Truck? truck { get; set; }
        [ForeignKey("truck")]
        public int? truckId { get;set; }

    }
}
