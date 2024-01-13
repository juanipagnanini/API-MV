using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class VillaNumberUpdateDto
    {
        [Required]
        public int VillaNum { get; set; }

        [Required]
        public int VillaId { get; set; }

        public string EspecialDetail { get; set; }
    }
}
