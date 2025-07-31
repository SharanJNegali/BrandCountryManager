using System.ComponentModel.DataAnnotations;

namespace BrandCountryManager.Models.DTOs
{
    public class UpdateBrandDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public int CountryId { get; set; }
    }
}