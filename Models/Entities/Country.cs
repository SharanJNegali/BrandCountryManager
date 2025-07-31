using System.ComponentModel.DataAnnotations;

namespace BrandCountryManager.Models.Entities
{
    public class Country
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(3)]
        public string IsoCode { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        // Navigation property
        public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();
    }
}