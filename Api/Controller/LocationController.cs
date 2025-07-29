using Application.DTOs.Location;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var location = await _locationService.GetById(id);
            if (location == null)
            {
                return NotFound(new { message = $"La ubicacion con el ID:{id} no fue encontado" });
            }
            try
            {
                return Ok(location);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado", ProblemDetails = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Add([FromBody] LocationInsertDto locationInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var location = await _locationService.Add(locationInsertDto);
                return CreatedAtAction(nameof(GetById), new { id = location.Id }, location);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = "Operacion invalida. ", ProblemDetails = ex.Message });
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

        public async Task<IActionResult> Update(int id, [FromBody] LocationUpdateDto locationUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != locationUpdateDto.Id)
            {
                return BadRequest(new { message = "El ID de la ruta no coincide con el ID de la ubicacion en el cuerpo de la solicitud." });
            }
            try
            {
                var locationUpdate = await _locationService.Update(locationUpdateDto);
                if (!locationUpdate)
                {
                    return BadRequest(new { message = $"No se encontró una ubicacion con el ID {id} para actualizar." });
                }
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
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
                var locationToDelete = await _locationService.Delete(id);
                if (!locationToDelete)
                {
                    return NotFound(new { message = $"No se encontro una ubicacion con el ID:{id} para eliminar" });
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