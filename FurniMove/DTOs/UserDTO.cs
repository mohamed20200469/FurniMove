namespace FurniMove.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public int MoveCounter { get; set; }
        public string? UserImgURL { get; set; }
        public bool Suspended { get; set; }
    }
}
