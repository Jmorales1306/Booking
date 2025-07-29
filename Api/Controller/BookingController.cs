using Application.DTOs.Booking;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _bookingService.GetById(id);
            if (booking == null)
            {
                return NotFound(new { message = $"La reserva con el ID:{id} no fue encontrado " });
            }
            try
            {
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado", ProblemDetails = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] BookingInsertDto bookingInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var booking = await _bookingService.Add(bookingInsertDto);
                return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = "Operacion invalida", ProblemDetails = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado", ProblemDetails = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BookingUpdateDto bookingUpdateDto)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            if (id != bookingUpdateDto.Id)
            {

                return BadRequest(new { message = "El ID de la ruta no coincide con el ID de la reserva en el cuerpo de la solicitud." });
            }
            try
            {

                var bookingUpdate = await _bookingService.Update(bookingUpdateDto);
                if (!bookingUpdate)
                {
                    return BadRequest(new
                    {
                        message = $"No se encontró una Reserva con el ID {id} para actualizar."
                    });
                }
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Oops. Ocurrió un error inesperado al procesar la reserva.", ProblemDetails = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var bookingToDelete = await _bookingService.Delete(id);
                if (!bookingToDelete)
                {
                    return NotFound(new { message = $"No se encontro la reservax` con el ID:{id} para eliminar." });
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado.", ProblemDetails = ex.Message });
            }
        }
    }
}