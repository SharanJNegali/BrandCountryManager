using MediatR;
using BrandCountryManager.Repositories;
using BrandCountryManager.Common.Exceptions;

namespace BrandCountryManager.Features.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, bool>
    {
        private readonly ICountryRepository _countryRepository;

        public DeleteCountryCommandHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<bool> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            if (!await _countryRepository.ExistsAsync(request.Id))
            {
                throw new NotFoundException("Country", request.Id);
            }

            return await _countryRepository.DeleteAsync(request.Id);
        }
    }
}