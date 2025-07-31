using BrandCountryManager.Models.Entities;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Mappers
{
    public static class BrandMapper
    {
        public static BrandDto ToDto(Brand brand)
        {
            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description,
                CountryId = brand.CountryId,
                CountryName = brand.Country?.Name ?? string.Empty,
                CountryIsoCode = brand.Country?.IsoCode ?? string.Empty,
                CreatedDate = brand.CreatedDate,
                UpdatedDate = brand.UpdatedDate
            };
        }

        public static IEnumerable<BrandDto> ToDto(IEnumerable<Brand> brands)
        {
            return brands.Select(ToDto);
        }

        public static Brand ToEntity(CreateBrandDto dto)
        {
            return new Brand
            {
                Name = dto.Name,
                Description = dto.Description,
                CountryId = dto.CountryId
            };
        }

        public static void UpdateEntity(Brand brand, UpdateBrandDto dto)
        {
            brand.Name = dto.Name;
            brand.Description = dto.Description;
            brand.CountryId = dto.CountryId;
        }
    }
}