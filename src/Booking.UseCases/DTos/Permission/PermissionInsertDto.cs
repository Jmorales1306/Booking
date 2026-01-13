namespace Booking.UseCases.DTOs.Permission
{
    public class PermissionInsertDto
    {
        [Required(ErrorMessage = "Se debe agregar un nombre al Permiso.")]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres.")]
        public required string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}