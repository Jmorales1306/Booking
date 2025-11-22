using Booking.UseCases.DTOs.Client;

namespace Booking.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;

        [HttpGet]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
        {
            var client = await _clientService.GetAll();
            return Ok(client);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var client = await _clientService.GetById(id);
                return Ok(client);
            }
            catch (KeyNotFoundException ex)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title: "Cliente no encontrado",
                    detail: ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] ClientInsertDto clientInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var client = await _clientService.Add(clientInsertDto);
                return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
            }
            catch (InvalidOperationException ex)
            {
                throw new DomainException(
                    ex.Message,
                    code: "CLIENT_CONFLICT");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ClientUpdateDto clientUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (id != clientUpdateDto.Id)
            {
                throw new DomainException(
                    "El ID de la ruta no coincide con el ID del cliente en el cuerpo de la solicitud.",
                    code: "ROUTE_BODY_ID_MISMATCH");
            }

            try
            {
                var clientUpdated = await _clientService.Update(clientUpdateDto);
                if (!clientUpdated)
                {
                    throw new DomainException(
                        $"No se encontró un cliente con el ID {id} para actualizar.",
                        code: "CLIENT_NOT_FOUND");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                throw new DomainException(
                    ex.Message,
                    code: "CLIENT_CONFLICT");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var clientDeleted = await _clientService.Delete(id);
            if (!clientDeleted)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title: "Cliente no encontrado",
                    detail: $"No se encontró un cliente con el ID:{id} para eliminar.");
            }

            return NoContent();
        }
    }
}