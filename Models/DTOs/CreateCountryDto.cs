using System.ComponentModel.DataAnnotations;

namespace BrandCountryManager.Models.DTOs
{
    public class CreateCountryDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(3, MinimumLength = 2)]
        public string IsoCode { get; set; } = string.Empty;
    }
}