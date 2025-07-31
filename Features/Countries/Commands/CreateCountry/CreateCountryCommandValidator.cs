using FluentValidation;

namespace BrandCountryManager.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(x => x.CountryDto.Name)
                .NotEmpty().WithMessage("Country name is required.")
                .Length(2, 100).WithMessage("Country name must be between 2 and 100 characters.");

            RuleFor(x => x.CountryDto.IsoCode)
                .NotEmpty().WithMessage("ISO code is required.")
                .Length(2, 3).WithMessage("ISO code must be between 2 and 3 characters.")
                .Matches("^[A-Z]+$").WithMessage("ISO code must contain only uppercase letters.");
        }
    }
}