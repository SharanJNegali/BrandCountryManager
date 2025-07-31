using MediatR;

namespace BrandCountryManager.Features.Brands.Commands.DeleteBrand
{
    public record DeleteBrandCommand(int Id) : IRequest<bool>;
}