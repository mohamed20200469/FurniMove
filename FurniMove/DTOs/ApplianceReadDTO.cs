using FurniMove.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class ApplianceReadDTO
    {
        public int Id { get; set; }
        public string? description { get; set; }
        public int? moveRequestId { get; set; }
        public string? ImgURL { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
