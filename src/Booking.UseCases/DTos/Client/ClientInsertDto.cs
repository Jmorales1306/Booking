namespace Booking.UseCases.DTOs.Client
{
    public class ClientInsertDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede sobrepasar los 50 caracteres")]
        public required string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El apellido no puede sobrepasar los 50 caracteres")]
        public required string LastName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El Email no puede sobrepasar los 100 caracteres")]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "El numero de telefono no puede superar los 20 caracteres")]
        [Phone(ErrorMessage = "Formato de número de teléfono inválido.")]
        public required string PhoneNumber { get; set; }
    }
}