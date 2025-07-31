using MediatR;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Features.Brands.Commands.UpdateBrand
{
    public record UpdateBrandCommand(int Id, UpdateBrandDto BrandDto) : IRequest<BrandDto>;
}