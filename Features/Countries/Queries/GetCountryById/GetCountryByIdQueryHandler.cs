using MediatR;
using BrandCountryManager.Models.DTOs;
using BrandCountryManager.Repositories;
using BrandCountryManager.Mappers;
using BrandCountryManager.Common.Exceptions;

namespace BrandCountryManager.Features.Countries.Queries.GetCountryById
{
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryDto>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountryByIdQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<CountryDto> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.GetByIdAsync(request.Id);
            if (country == null)
            {
                throw new NotFoundException("Country", request.Id);
            }

            return CountryMapper.ToDto(country);
        }
    }
}