using MediatR;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Features.Brands.Commands.CreateBrand
{
    public record CreateBrandCommand(CreateBrandDto BrandDto) : IRequest<BrandDto>;
}