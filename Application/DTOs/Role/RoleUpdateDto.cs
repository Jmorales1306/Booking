using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Role
{
    public class RoleUpdateDto
    {
        [Required(ErrorMessage = "El Id del Rol es requerido para realizar la actualizacion.")]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "El nombre del Rol no puede sobrepasar los 20 caracteres.")]
        public required string Name { get; set; }
    }
}