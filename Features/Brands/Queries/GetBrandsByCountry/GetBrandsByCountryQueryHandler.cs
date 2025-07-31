using MediatR;
using BrandCountryManager.Models.DTOs;
using BrandCountryManager.Repositories;
using BrandCountryManager.Mappers;
using BrandCountryManager.Common.Exceptions;

namespace BrandCountryManager.Features.Brands.Queries.GetBrandsByCountry
{
    public class GetBrandsByCountryQueryHandler : IRequestHandler<GetBrandsByCountryQuery, IEnumerable<BrandDto>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ICountryRepository _countryRepository;

        public GetBrandsByCountryQueryHandler(IBrandRepository brandRepository, ICountryRepository countryRepository)
        {
            _brandRepository = brandRepository;
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<BrandDto>> Handle(GetBrandsByCountryQuery request, CancellationToken cancellationToken)
        {
            // Validate that the country exists
            if (!await _countryRepository.ExistsAsync(request.CountryId))
            {
                throw new NotFoundException("Country", request.CountryId);
            }

            var brands = await _brandRepository.GetByCountryIdAsync(request.CountryId);
            return BrandMapper.ToDto(brands);
        }
    }
}