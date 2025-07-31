using BrandCountryManager.Models.Entities;

namespace BrandCountryManager.Repositories
{
    public interface ICountryRepository
    {
        Task<Country?> GetByIdAsync(int id);
        Task<Country?> GetByIsoCodeAsync(string isoCode);
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country> CreateAsync(Country country);
        Task<Country> UpdateAsync(Country country);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> IsoCodeExistsAsync(string isoCode, int? excludeId = null);
    }
}