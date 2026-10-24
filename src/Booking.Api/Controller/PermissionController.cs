using Booking.UseCases.DTOs.Permission;

namespace Booking.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController(IPermissionService permissionService) : ControllerBase
    {
        private readonly IPermissionService _permissionService = permissionService;

        [HttpGet]
        [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetAll()
        {
            var permission = await _permissionService.GetAll();
            return Ok(permission);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var permission = await _permissionService.GetById(id);
            if (permission == null)
            {
                return NotFound(new { message = $"El Permiso con el ID:{id} no fue encontado" });
            }
            try
            {
                return Ok(permission);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado", ProblemDetails = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] PermissionInsertDto permissionInsertDTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var permission = await _permissionService.Add(permissionInsertDTo);
                return CreatedAtAction(nameof(GetById), new { id = permission.Id }, permission);
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] PermissionUpdateDto permissionUpdateDTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != permissionUpdateDTo.Id)
            {
                return BadRequest(new { message = "El ID de la ruta no coincide con el ID del Permiso en el cuerpo de la solicitud." });

            }
            try
            {
                var permissionUpdate = await _permissionService.Update(permissionUpdateDTo);
                if (!permissionUpdate)
                {
                    return BadRequest(new { message = $"No se encontró un Permiso con el ID {id} para actualizar." });
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
                var permissionToDelete = await _permissionService.Delete(id);
                if (!permissionToDelete)
                {
                    return NotFound(new { message = $"No se encontro un Permiso con el ID:{id} para eliminar" });
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
