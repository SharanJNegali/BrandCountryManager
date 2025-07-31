using MediatR;
using BrandCountryManager.Models.DTOs;
using BrandCountryManager.Repositories;
using BrandCountryManager.Mappers;
using BrandCountryManager.Common.Exceptions;

namespace BrandCountryManager.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ICountryRepository _countryRepository;

        public UpdateBrandCommandHandler(IBrandRepository brandRepository, ICountryRepository countryRepository)
        {
            _brandRepository = brandRepository;
            _countryRepository = countryRepository;
        }

        public async Task<BrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetByIdAsync(request.Id);
            if (brand == null)
            {
                throw new NotFoundException(nameof(brand), request.Id);
            }

            // Validate that the country exists
            if (!await _countryRepository.ExistsAsync(request.BrandDto.CountryId))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { nameof(request.BrandDto.CountryId), new[] { $"Country with ID '{request.BrandDto.CountryId}' does not exist." } }
                });
            }

            BrandMapper.UpdateEntity(brand, request.BrandDto);
            var updatedBrand = await _brandRepository.UpdateAsync(brand);
            
            return BrandMapper.ToDto(updatedBrand);
        }
    }
}