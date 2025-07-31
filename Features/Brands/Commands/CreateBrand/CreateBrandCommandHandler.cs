using MediatR;
using BrandCountryManager.Models.DTOs;
using BrandCountryManager.Repositories;
using BrandCountryManager.Mappers;
using BrandCountryManager.Common.Exceptions;

namespace BrandCountryManager.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ICountryRepository _countryRepository;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, ICountryRepository countryRepository)
        {
            _brandRepository = brandRepository;
            _countryRepository = countryRepository;
        }

        public async Task<BrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            // Validate that the country exists
            if (!await _countryRepository.ExistsAsync(request.BrandDto.CountryId))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { nameof(request.BrandDto.CountryId), new[] { $"Country with ID '{request.BrandDto.CountryId}' does not exist." } }
                });
            }

            var brand = BrandMapper.ToEntity(request.BrandDto);
            var createdBrand = await _brandRepository.CreateAsync(brand);
            
            return BrandMapper.ToDto(createdBrand);
        }
    }
}