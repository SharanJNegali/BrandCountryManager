using MediatR;
using BrandCountryManager.Repositories;
using BrandCountryManager.Common.Exceptions;

namespace BrandCountryManager.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            if (!await _brandRepository.ExistsAsync(request.Id))
            {
                throw new NotFoundException("Brand", request.Id);
            }

            return await _brandRepository.DeleteAsync(request.Id);
        }
    }
}