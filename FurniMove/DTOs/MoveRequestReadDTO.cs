using FurniMove.Models;
using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class MoveRequestReadDTO
    {
        public int Id { get; set; }
        public Location? StartLocation { get; set; }
        public Location? EndLocation { get; set; }
        public string? CustomerId { get; set; }
        public UserDTO? Customer { get; set; }
        public string? ServiceProviderId {  get; set; }
        public UserDTO? ServiceProvider { get; set; }
        public int? TruckId { get; set; }
        public Truck? Truck { get; set; }
        public string? Status { get; set; }
        public DateOnly StartDate { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Rating { get; set; }
        public double? Cost { get; set; }
        public int NumOfAppliances { get; set; }
        public string? StartAddress { get; set; }
        public string? EndAddress { get; set; }
        public double Distance { get; set; }
        public float ETA { get; set; }
        public string? VehicleType { get; set; }
    }
}
