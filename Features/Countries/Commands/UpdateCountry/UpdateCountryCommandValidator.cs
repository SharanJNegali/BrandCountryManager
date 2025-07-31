using FluentValidation;

namespace BrandCountryManager.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Country ID must be greater than 0.");

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