using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User
{
    public class UserInsertDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "El Nombre no puede exceder los 50 caracteres.")]
        public required string FirstName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "El Apellido no puede exceder los 50 caracteres.")]
        public required string LastName { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "El Email no puede superar los 100 caracteres.")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "El ID del Rol es obligatorio.")]
        public required int RoleId { get; set; }
    }
}