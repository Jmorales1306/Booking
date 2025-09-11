
using Booking.UseCases.DTOs.Room;

namespace Booking.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController(IRoomService roomService) : ControllerBase
    {
        private readonly IRoomService _roomService = roomService;

        [HttpGet]
        [ProducesResponseType(typeof(RoomDTo), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RoomDTo>>> GetAll()
        {
            var room = await _roomService.GetAll();
            return Ok(room);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoomDTo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _roomService.GetById(id);
            if (room == null)
            {
                return NotFound(new { message = $"La Sala con el ID:{id} no fue encontado" });
            }
            try
            {
                return Ok(room);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado", ProblemDetails = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(RoomDTo), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] RoomInsertDto roomInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var room = await _roomService.Add(roomInsertDto);
                return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
            }

            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = "Operacion invalida. ", ProblemDetails = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = "Operación inválida. " + ex.Message, ProblemDetails = ex.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado", ProblemDetails = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] RoomUpdateDto roomUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != roomUpdateDto.Id)
            {
                return BadRequest(new { message = "El ID de la ruta no coincide con el ID de la Sala en el cuerpo de la solicitud." });

            }
            try
            {
                var roomUpdate = await _roomService.Update(roomUpdateDto);
                if (!roomUpdate)
                {
                    return BadRequest(new { message = $"No se encontró una Sala con el ID {id} para actualizar." });
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado", ProblemDetails = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var roomToDelete = await _roomService.Delete(id);
                if (!roomToDelete)
                {
                    return NotFound(new { message = $"No se encontro una Sala con el ID:{id} para eliminar" });
                }
                return NoContent();
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(new { message = ex });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado", ProblemDetails = ex.Message });
            }
        }

    }
}