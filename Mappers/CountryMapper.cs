using BrandCountryManager.Models.Entities;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Mappers
{
    public static class CountryMapper
    {
        public static CountryDto ToDto(Country country)
        {
            return new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
                IsoCode = country.IsoCode,
                CreatedDate = country.CreatedDate,
                UpdatedDate = country.UpdatedDate
            };
        }

        public static IEnumerable<CountryDto> ToDto(IEnumerable<Country> countries)
        {
            return countries.Select(ToDto);
        }

        public static Country ToEntity(CreateCountryDto dto)
        {
            return new Country
            {
                Name = dto.Name,
                IsoCode = dto.IsoCode.ToUpperInvariant()
            };
        }

        public static void UpdateEntity(Country country, UpdateCountryDto dto)
        {
            country.Name = dto.Name;
            country.IsoCode = dto.IsoCode.ToUpperInvariant();
        }
    }
}