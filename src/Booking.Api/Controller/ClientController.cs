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
            var client = await _clientService.GetById(id);
            return Ok(client);
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

            var client = await _clientService.Add(clientInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
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
                    "ROUTE_BODY_ID_MISMATCH",
                    target: "id");
            }

            var clientUpdated = await _clientService.Update(clientUpdateDto);
            if (!clientUpdated)
            {
                throw new DomainException(
                    "CLIENT_NOT_FOUND",
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
            var clientDeleted = await _clientService.Delete(id);
            if (!clientDeleted)
            {
                throw new DomainException(
                    "CLIENT_NOT_FOUND",
                    target: "id");
            }

            return NoContent();
        }
    }
}