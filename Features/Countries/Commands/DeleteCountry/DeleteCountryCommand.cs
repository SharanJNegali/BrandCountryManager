using MediatR;

namespace BrandCountryManager.Features.Countries.Commands.DeleteCountry
{
    public record DeleteCountryCommand(int Id) : IRequest<bool>;
}