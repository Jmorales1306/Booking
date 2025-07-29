using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Permission
{
    public class PermissionDTo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Se debe agregar un nombre al Permiso.")]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres.")]
        public required string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}