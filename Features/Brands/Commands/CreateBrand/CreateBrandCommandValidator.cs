using FluentValidation;

namespace BrandCountryManager.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.BrandDto.Name)
                .NotEmpty().WithMessage("Brand name is required.")
                .Length(2, 100).WithMessage("Brand name must be between 2 and 100 characters.");

            RuleFor(x => x.BrandDto.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.BrandDto.CountryId)
                .GreaterThan(0).WithMessage("Country ID must be greater than 0.");
        }
    }
}