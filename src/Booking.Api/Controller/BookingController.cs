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
            var booking = await _bookingService.GetById(id);
            return Ok(booking);
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

            var booking = await _bookingService.Add(bookingInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
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
                    "ROUTE_BODY_ID_MISMATCH",
                    target: "id");
            }

            var bookingUpdated = await _bookingService.Update(bookingUpdateDto);

            if (!bookingUpdated)
            {
                throw new DomainException(
                    "BOOKING_NOT_FOUND",
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
            var bookingToDelete = await _bookingService.Delete(id);
            if (!bookingToDelete)
            {
                throw new DomainException(
                    "BOOKING_NOT_FOUND",
                    target: "id");
            }

            return NoContent();
        }
    }
}