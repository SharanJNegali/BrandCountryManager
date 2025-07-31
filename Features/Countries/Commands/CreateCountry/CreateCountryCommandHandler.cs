using MediatR;
using BrandCountryManager.Models.DTOs;
using BrandCountryManager.Repositories;
using BrandCountryManager.Mappers;
using BrandCountryManager.Common.Exceptions;

namespace BrandCountryManager.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CountryDto>
    {
        private readonly ICountryRepository _countryRepository;

        public CreateCountryCommandHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<CountryDto> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            // Check if ISO code already exists
            if (await _countryRepository.IsoCodeExistsAsync(request.CountryDto.IsoCode.ToUpperInvariant()))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { nameof(request.CountryDto.IsoCode), new[] { $"ISO code '{request.CountryDto.IsoCode}' already exists." } }
                });
            }

            var country = CountryMapper.ToEntity(request.CountryDto);
            var createdCountry = await _countryRepository.CreateAsync(country);
            
            return CountryMapper.ToDto(createdCountry);
        }
    }
}