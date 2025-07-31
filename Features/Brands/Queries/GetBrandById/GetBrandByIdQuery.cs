using MediatR;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Features.Brands.Queries.GetBrandById
{
    public record GetBrandByIdQuery(int Id) : IRequest<BrandDto>;
}