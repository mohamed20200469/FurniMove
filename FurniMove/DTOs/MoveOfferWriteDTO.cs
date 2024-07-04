using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class MoveOfferWriteDTO
    {
        [Required]
        public int price { get; set; }
        [Required]
        public int moveRequestId { get; set; }
    }
}
