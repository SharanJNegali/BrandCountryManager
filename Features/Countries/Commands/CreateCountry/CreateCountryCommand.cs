using MediatR;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Features.Countries.Commands.CreateCountry
{
    public record CreateCountryCommand(CreateCountryDto CountryDto) : IRequest<CountryDto>;
}