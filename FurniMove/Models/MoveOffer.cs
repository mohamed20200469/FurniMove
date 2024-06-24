using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurniMove.Models
{
    public class MoveOffer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Price { get; set; }
        public MoveRequest? MoveRequest { get; set; }
        [ForeignKey("MoveRequest")]
        public int? MoveRequestId { get; set; }
        public AppUser? ServiceProvider { get; set; }
        [ForeignKey("ServiceProvider")]
        public string? ServiceProviderId { get; set; }
        public bool Accepted { get; set; } = false;
    }
}
