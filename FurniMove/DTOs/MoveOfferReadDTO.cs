namespace FurniMove.DTOs
{
    public class MoveOfferReadDTO
    {
        public int Id { get; set; }
        public string? ServiceProviderId { get; set; }
        public UserDTO? ServiceProvider { get; set; }
        public int Price { get; set; }
        public int? MoveRequestId { get; set; }
        public bool Accepted { get; set; }
    }
}
