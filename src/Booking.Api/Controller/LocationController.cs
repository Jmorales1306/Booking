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
            try
            {
                var location = await _locationService.GetById(id);
                return Ok(location);
            }
            catch (KeyNotFoundException ex)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title: "Ubicación no encontrada",
                    detail: ex.Message);
            }
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

            try
            {
                var location = await _locationService.Add(locationInsertDto);
                return CreatedAtAction(nameof(GetById), new { id = location.Id }, location);
            }
            catch (InvalidOperationException ex)
            {
                throw new DomainException(
                    ex.Message,
                    code: "LOCATION_CONFLICT");
            }
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
                    "El ID de la ruta no coincide con el ID de la ubicación en el cuerpo de la solicitud.",
                    code: "ROUTE_BODY_ID_MISMATCH");
            }

            try
            {
                var locationUpdated = await _locationService.Update(locationUpdateDto);
                if (!locationUpdated)
                {
                    throw new DomainException(
                        $"No se encontró una ubicación con el ID {id} para actualizar.",
                        code: "LOCATION_NOT_FOUND");
                }

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                throw new DomainException(
                    ex.Message,
                    code: "LOCATION_NOT_FOUND");
            }
            catch (InvalidOperationException ex)
            {
                throw new DomainException(
                    ex.Message,
                    code: "LOCATION_CONFLICT");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var locationDeleted = await _locationService.Delete(id);
                if (!locationDeleted)
                {
                    return Problem(
                        statusCode: StatusCodes.Status404NotFound,
                        type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        title: "Ubicación no encontrada",
                        detail: $"No se encontró una ubicación con el ID:{id} para eliminar.");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title: "Ubicación no encontrada",
                    detail: ex.Message);
            }
        }
    }
}