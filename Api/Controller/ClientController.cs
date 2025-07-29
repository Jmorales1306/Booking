using Application.DTOs.Client;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;

        [HttpGet]
        [ProducesResponseType(typeof(ClientDTo), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClientDTo>>> GetAll()
        {
            var client = await _clientService.GetAll();
            return Ok(client);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientDTo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _clientService.GetById(id);
            if (client == null)
            {
                return NotFound(new { message = $"El Client con el ID:{id} no fue encontrado " });
            }
            try
            {
                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado", ProblemDetails = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientDTo), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] ClientInsertDto clientInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var client = await _clientService.Add(clientInsertDto);
                return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
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
        public async Task<IActionResult> Update(int id, [FromBody] ClientUpdateDto clientUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != clientUpdateDto.Id)
            {
                return BadRequest(new { message = "El ID de la ruta no coincide con el ID del cliente en el cuerpo de la solicitud." });
            }
            try
            {
                var clientUpdate = await _clientService.Update(clientUpdateDto);
                if (!clientUpdate)
                {
                    return BadRequest(new
                    {
                        message = $"No se encontró un cliente con el ID {id} para actualizar."
                    });
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var clientDelete = await _clientService.Delete(id);
                if (!clientDelete)
                {
                    return NotFound(new { message = $"No se encontro un cliente con el ID:{id} para eliminar." });
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
