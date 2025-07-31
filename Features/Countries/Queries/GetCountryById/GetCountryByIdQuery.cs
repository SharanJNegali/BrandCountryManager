using MediatR;
using BrandCountryManager.Models.DTOs;

namespace BrandCountryManager.Features.Countries.Queries.GetCountryById
{
    public record GetCountryByIdQuery(int Id) : IRequest<CountryDto>;
}