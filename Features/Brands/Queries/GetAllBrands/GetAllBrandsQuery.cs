using MediatR;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Features.Brands.Queries.GetAllBrands
{
    public record GetAllBrandsQuery : IRequest<IEnumerable<BrandDto>>;
}