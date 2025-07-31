using MediatR;
using Microsoft.AspNetCore.Mvc;
using BrandCountryManager.Models.DTOs;
using BrandCountryManager.Features.Brands.Commands.CreateBrand;
using BrandCountryManager.Features.Brands.Commands.UpdateBrand;
using BrandCountryManager.Features.Brands.Commands.DeleteBrand;
using BrandCountryManager.Features.Brands.Queries.GetBrandById;
using BrandCountryManager.Features.Brands.Queries.GetBrandsByCountry;
using BrandCountryManager.Features.Brands.Queries.GetAllBrands;

namespace BrandCountryManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all brands
        /// </summary>
        /// <returns>List of all brands</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BrandDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _mediator.Send(new GetAllBrandsQuery());
            return Ok(brands);
        }

        /// <summary>
        /// Get brand by ID
        /// </summary>
        /// <param name="id">Brand ID</param>
        /// <returns>Brand details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BrandDto>> GetBrandById(int id)
        {
            var brand = await _mediator.Send(new GetBrandByIdQuery(id));
            return Ok(brand);
        }

        /// <summary>
        /// Get brands by country
        /// </summary>
        /// <param name="countryId">Country ID</param>
        /// <returns>List of brands in the specified country</returns>
        [HttpGet("country/{countryId}")]
        [ProducesResponseType(typeof(IEnumerable<BrandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsByCountry(int countryId)
        {
            var brands = await _mediator.Send(new GetBrandsByCountryQuery(countryId));
            return Ok(brands);
        }

        /// <summary>
        /// Create a new brand
        /// </summary>
        /// <param name="createBrandDto">Brand creation data</param>
        /// <returns>Created brand</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BrandDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BrandDto>> CreateBrand([FromBody] CreateBrandDto createBrandDto)
        {
            var brand = await _mediator.Send(new CreateBrandCommand(createBrandDto));
            return CreatedAtAction(nameof(GetBrandById), new { id = brand.Id }, brand);
        }

        /// <summary>
        /// Update an existing brand
        /// </summary>
        /// <param name="id">Brand ID</param>
        /// <param name="updateBrandDto">Brand update data</param>
        /// <returns>Updated brand</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BrandDto>> UpdateBrand(int id, [FromBody] UpdateBrandDto updateBrandDto)
        {
            var brand = await _mediator.Send(new UpdateBrandCommand(id, updateBrandDto));
            return Ok(brand);
        }

        /// <summary>
        /// Delete a brand
        /// </summary>
        /// <param name="id">Brand ID</param>
        /// <returns>Success status</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _mediator.Send(new DeleteBrandCommand(id));
            return NoContent();
        }
    }
}