using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurniMove.Models
{
    public class MoveOffer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int price { get; set; }
        public MoveRequest? moveRequest { get; set; }
        [ForeignKey("moveRequest")]
        public int? moveRequestId { get; set; }
        public AppUser? serviceProvider { get; set; }
        [ForeignKey("serviceProvider")]
        public string? serviceProviderId { get; set; }
    }
}
