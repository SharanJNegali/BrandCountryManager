using MediatR;
using Microsoft.AspNetCore.Mvc;
using BrandCountryManager.Models.DTOs;
using BrandCountryManager.Features.Countries.Commands.CreateCountry;
using BrandCountryManager.Features.Countries.Commands.UpdateCountry;
using BrandCountryManager.Features.Countries.Commands.DeleteCountry;
using BrandCountryManager.Features.Countries.Queries.GetCountryById;
using BrandCountryManager.Features.Countries.Queries.GetAllCountries;

namespace BrandCountryManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns>List of all countries</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CountryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAllCountries()
        {
            var countries = await _mediator.Send(new GetAllCountriesQuery());
            return Ok(countries);
        }

        /// <summary>
        /// Get country by ID
        /// </summary>
        /// <param name="id">Country ID</param>
        /// <returns>Country details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryDto>> GetCountryById(int id)
        {
            var country = await _mediator.Send(new GetCountryByIdQuery(id));
            return Ok(country);
        }

        /// <summary>
        /// Create a new country
        /// </summary>
        /// <param name="createCountryDto">Country creation data</param>
        /// <returns>Created country</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountryDto>> CreateCountry([FromBody] CreateCountryDto createCountryDto)
        {
            var country = await _mediator.Send(new CreateCountryCommand(createCountryDto));
            return CreatedAtAction(nameof(GetCountryById), new { id = country.Id }, country);
        }

        /// <summary>
        /// Update an existing country
        /// </summary>
        /// <param name="id">Country ID</param>
        /// <param name="updateCountryDto">Country update data</param>
        /// <returns>Updated country</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryDto>> UpdateCountry(int id, [FromBody] UpdateCountryDto updateCountryDto)
        {
            var country = await _mediator.Send(new UpdateCountryCommand(id, updateCountryDto));
            return Ok(country);
        }

        /// <summary>
        /// Delete a country
        /// </summary>
        /// <param name="id">Country ID</param>
        /// <returns>Success status</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _mediator.Send(new DeleteCountryCommand(id));
            return NoContent();
        }
    }
}