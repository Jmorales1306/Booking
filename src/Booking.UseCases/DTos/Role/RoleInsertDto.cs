namespace Booking.UseCases.DTOs.Role
{
    public class RoleInsertDto
    {
        [Required]
        [StringLength(20, ErrorMessage = "El nombre del Rol no puede sobrepasar los 20 caracteres.")]
        public required string Name { get; set; }
    }
}