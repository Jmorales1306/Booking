using Booking.UseCases.DTOs.Location;

namespace Booking.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController(ILocationService locationService) : ControllerBase
    {
        private readonly ILocationService _locationService = locationService;

        [HttpGet]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
        {
            var location = await _locationService.GetAll();
            return Ok(location);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var location = await _locationService.GetById(id);
            return Ok(location);
        }

        [HttpPost]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] LocationInsertDto locationInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var location = await _locationService.Add(locationInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = location.Id }, location);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] LocationUpdateDto locationUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (id != locationUpdateDto.Id)
            {
                throw new DomainException(
                    "ROUTE_BODY_ID_MISMATCH",
                    target: "id");
            }

            var locationUpdated = await _locationService.Update(locationUpdateDto);
            if (!locationUpdated)
            {
                throw new DomainException(
                    "LOCATION_NOT_FOUND",
                    target: "id");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var locationDeleted = await _locationService.Delete(id);
            if (!locationDeleted)
            {
                throw new DomainException(
                    "LOCATION_NOT_FOUND",
                    target: "id");
            }

            return NoContent();
        }
    }
}