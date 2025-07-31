using MediatR;
using BrandCountryManager.Models.DTOs;
using BrandCountryManager.Repositories;
using BrandCountryManager.Mappers;
using BrandCountryManager.Common.Exceptions;

namespace BrandCountryManager.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CountryDto>
    {
        private readonly ICountryRepository _countryRepository;

        public UpdateCountryCommandHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<CountryDto> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.GetByIdAsync(request.Id);
            if (country == null)
            {
                throw new NotFoundException(nameof(country), request.Id);
            }

            // Check if ISO code already exists for another country
            if (await _countryRepository.IsoCodeExistsAsync(request.CountryDto.IsoCode.ToUpperInvariant(), request.Id))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { nameof(request.CountryDto.IsoCode), new[] { $"ISO code '{request.CountryDto.IsoCode}' already exists." } }
                });
            }

            CountryMapper.UpdateEntity(country, request.CountryDto);
            var updatedCountry = await _countryRepository.UpdateAsync(country);
            
            return CountryMapper.ToDto(updatedCountry);
        }
    }
}