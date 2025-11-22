using Booking.UseCases.DTOs.Booking;

namespace Booking.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController(IBookingService bookingService) : ControllerBase
    {
        private readonly IBookingService _bookingService = bookingService;

        [HttpGet]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll()
        {
            var booking = await _bookingService.GetAll();
            return Ok(booking);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var booking = await _bookingService.GetById(id);
                return Ok(booking);
            }
            catch (KeyNotFoundException ex)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title: "Reserva no encontrada",
                    detail: ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] BookingInsertDto bookingInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var booking = await _bookingService.Add(bookingInsertDto);
                return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
            }
            catch (KeyNotFoundException ex)
            {
                // Sala/Usuario/Cliente relacionados no existen → error de dominio
                throw new DomainException(
                    ex.Message,
                    code: "RELATED_ENTITY_NOT_FOUND");
            }
            catch (InvalidOperationException ex)
            {
                // Validaciones de hora/fecha → error de dominio
                throw new DomainException(
                    ex.Message,
                    code: "BOOKING_VALIDATION_ERROR");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BookingUpdateDto bookingUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (id != bookingUpdateDto.Id)
            {
                throw new DomainException(
                    "El ID de la ruta no coincide con el ID de la reserva en el cuerpo de la solicitud.",
                    code: "ROUTE_BODY_ID_MISMATCH");
            }

            try
            {
                var bookingUpdated = await _bookingService.Update(bookingUpdateDto);

                if (!bookingUpdated)
                {
                    throw new DomainException(
                        $"No se encontró una Reserva con el ID {id} para actualizar.",
                        code: "BOOKING_NOT_FOUND");
                }

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // IDs relacionados (Room/User/Client) que no existen
                throw new DomainException(
                    ex.Message,
                    code: "RELATED_ENTITY_NOT_FOUND");
            }
            catch (InvalidOperationException ex)
            {
                throw new DomainException(
                    ex.Message,
                    code: "BOOKING_VALIDATION_ERROR");
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
                var bookingToDelete = await _bookingService.Delete(id);
                if (!bookingToDelete)
                {
                    return Problem(
                        statusCode: StatusCodes.Status404NotFound,
                        type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        title: "Reserva no encontrada",
                        detail: $"No se encontró la reserva con el ID:{id} para eliminar.");
                }

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title: "Reserva no encontrada",
                    detail: ex.Message);
            }
        }
    }
}