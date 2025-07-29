using Application.DTOs.Role;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpGet]
        [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            var role = await _roleService.GetAll();
            return Ok(role);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _roleService.GetById(id);
            if (role == null)
            {
                return NotFound(new { message = $"El Rol con el ID:{id} no fue encontado" });
            }

            try
            {
                return Ok(role);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Opss. Ocurrio un error inesperado", ProblemDetails = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(RoleDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Add([FromBody] RoleInsertDto roleInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var role = await _roleService.Add(roleInsertDto);
                return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);

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

        public async Task<IActionResult> Update(int id, [FromBody] RoleUpdateDto roleUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != roleUpdateDto.Id)
            {
                return BadRequest(new { message = "El ID de la ruta no coincide con el ID del Role en el cuerpo de la solicitud." });
            }
            try
            {
                var roleUpdate = await _roleService.Update(roleUpdateDto);
                if (!roleUpdate)
                {
                    return BadRequest(new { message = $"No se encontró un Rol con el ID {id} para actualizar." });
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
                var roleDelete = await _roleService.Delete(id);
                if (!roleDelete)
                {
                    return NotFound(new { message = $"No se encontro un Rol con el ID:{id} para eliminar" });
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