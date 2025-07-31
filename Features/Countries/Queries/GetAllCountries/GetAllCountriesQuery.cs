using MediatR;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Features.Countries.Queries.GetAllCountries
{
    public record GetAllCountriesQuery : IRequest<IEnumerable<CountryDto>>;
}