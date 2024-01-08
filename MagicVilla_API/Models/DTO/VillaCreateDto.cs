using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class VillaCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Occupants { get; set; }
        public int SquareMeters { get; set; }
        public string ImagenUrl { get; set; }
        public string Services { get; set; }
    }
}
