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
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var permission = await _permissionService.GetById(id);
                return Ok(permission);
            }
            catch (KeyNotFoundException ex)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title: "Permiso no encontrado",
                    detail: ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] PermissionInsertDto permissionInsertDTo)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var permission = await _permissionService.Add(permissionInsertDTo);
                return CreatedAtAction(nameof(GetById), new { id = permission.Id }, permission);
            }
            catch (InvalidOperationException ex)
            {
                throw new DomainException(
                    ex.Message,
                    code: "PERMISSION_CONFLICT");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] PermissionUpdateDto permissionUpdateDTo)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (id != permissionUpdateDTo.Id)
            {
                throw new DomainException(
                    "El ID de la ruta no coincide con el ID del Permiso en el cuerpo de la solicitud.",
                    code: "ROUTE_BODY_ID_MISMATCH");
            }

            try
            {
                var permissionUpdated = await _permissionService.Update(permissionUpdateDTo);
                if (!permissionUpdated)
                {
                    throw new DomainException(
                        $"No se encontró un Permiso con el ID {id} para actualizar.",
                        code: "PERMISSION_NOT_FOUND");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Incluye tanto "no encontrado" como conflictos, según tu servicio actual
                throw new DomainException(
                    ex.Message,
                    code: "PERMISSION_CONFLICT");
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
                var permissionDeleted = await _permissionService.Delete(id);
                if (!permissionDeleted)
                {
                    return Problem(
                        statusCode: StatusCodes.Status404NotFound,
                        type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        title: "Permiso no encontrado",
                        detail: $"No se encontró un Permiso con el ID:{id} para eliminar.");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title: "Permiso no encontrado",
                    detail: ex.Message);
            }
        }
    }
}