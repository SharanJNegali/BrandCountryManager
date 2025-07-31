using MediatR;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Features.Brands.Queries.GetBrandsByCountry
{
    public record GetBrandsByCountryQuery(int CountryId) : IRequest<IEnumerable<BrandDto>>;
}