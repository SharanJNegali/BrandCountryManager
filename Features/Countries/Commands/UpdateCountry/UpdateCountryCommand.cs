using MediatR;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Features.Countries.Commands.UpdateCountry
{
    public record UpdateCountryCommand(int Id, UpdateCountryDto CountryDto) : IRequest<CountryDto>;
}