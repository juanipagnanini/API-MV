using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTO
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Occupants { get; set; }
        [Required]
        public int SquareMeters { get; set; }
        [Required]
        public string ImagenUrl { get; set; }
        public string Services { get; set; }
    }
}
