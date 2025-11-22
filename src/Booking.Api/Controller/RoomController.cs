
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
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _roomService.GetById(id);

            if (room == null)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title: "Sala no encontrada",
                    detail: $"La Sala con el ID:{id} no fue encontrada.");
            }

            return Ok(room);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RoomDTo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] RoomInsertDto roomInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var room = await _roomService.Add(roomInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] RoomUpdateDto roomUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (id != roomUpdateDto.Id)
            {
                throw new DomainException(
                    "El ID de la ruta no coincide con el ID de la Sala en el cuerpo de la solicitud.",
                    code: "ROUTE_BODY_ID_MISMATCH");
            }

            var roomUpdated = await _roomService.Update(roomUpdateDto);

            if (!roomUpdated)
            {
                throw new DomainException(
                    $"No se encontró una Sala con el ID {id} para actualizar.",
                    code: "ROOM_NOT_FOUND");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var roomDeleted = await _roomService.Delete(id);

            if (!roomDeleted)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title: "Sala no encontrada",
                    detail: $"No se encontró una Sala con el ID:{id} para eliminar.");
            }

            return NoContent();
        }

    }
}
