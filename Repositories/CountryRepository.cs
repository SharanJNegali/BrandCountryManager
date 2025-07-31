using Microsoft.EntityFrameworkCore;
using BrandCountryManager.Data;
using BrandCountryManager.Models.Entities;

namespace BrandCountryManager.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            return await _context.Countries
                .Include(c => c.Brands)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Country?> GetByIsoCodeAsync(string isoCode)
        {
            return await _context.Countries
                .Include(c => c.Brands)
                .FirstOrDefaultAsync(c => c.IsoCode == isoCode);
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _context.Countries
                .Include(c => c.Brands)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Country> CreateAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<Country> UpdateAsync(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
                return false;

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Countries.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> IsoCodeExistsAsync(string isoCode, int? excludeId = null)
        {
            var query = _context.Countries.Where(c => c.IsoCode == isoCode);
            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }
    }
}