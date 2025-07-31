using Microsoft.EntityFrameworkCore;
using BrandCountryManager.Data;
using BrandCountryManager.Models.Entities;

namespace BrandCountryManager.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await _context.Brands
                .Include(b => b.Country)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands
                .Include(b => b.Country)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Brand>> GetByCountryIdAsync(int countryId)
        {
            return await _context.Brands
                .Include(b => b.Country)
                .Where(b => b.CountryId == countryId)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public async Task<Brand> CreateAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            
            // Reload with country information
            return await GetByIdAsync(brand.Id) ?? brand;
        }

        public async Task<Brand> UpdateAsync(Brand brand)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
            
            // Reload with country information
            return await GetByIdAsync(brand.Id) ?? brand;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
                return false;

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Brands.AnyAsync(b => b.Id == id);
        }
    }
}