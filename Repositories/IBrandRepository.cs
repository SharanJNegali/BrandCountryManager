using BrandCountryManager.Models.Entities;

namespace BrandCountryManager.Repositories
{
    public interface IBrandRepository
    {
        Task<Brand?> GetByIdAsync(int id);
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<IEnumerable<Brand>> GetByCountryIdAsync(int countryId);
        Task<Brand> CreateAsync(Brand brand);
        Task<Brand> UpdateAsync(Brand brand);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}