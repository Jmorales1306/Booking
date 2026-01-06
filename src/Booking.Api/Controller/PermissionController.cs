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
            var permission = await _permissionService.GetById(id);
            return Ok(permission);
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

            var permission = await _permissionService.Add(permissionInsertDTo);
            return CreatedAtAction(nameof(GetById), new { id = permission.Id }, permission);
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
                    "ROUTE_BODY_ID_MISMATCH",
                    target: "id");
            }

            var permissionUpdated = await _permissionService.Update(permissionUpdateDTo);
            if (!permissionUpdated)
            {
                throw new DomainException(
                    "PERMISSION_NOT_FOUND",
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
            var permissionDeleted = await _permissionService.Delete(id);
            if (!permissionDeleted)
            {
                throw new DomainException(
                    "PERMISSION_NOT_FOUND",
                    target: "id");
            }

            return NoContent();
        }
    }
}