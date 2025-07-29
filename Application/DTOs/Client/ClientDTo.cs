using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Client
{
    public class ClientDTo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede sobrepasar los 50 caracteres. ")]
        public required string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El apellido no puede sobrepasar los 50 caracteres. ")]
        public required string LastName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "El Email no puede sobrepasar los 100 caracteres. ")]
        public required string Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "El numero de telefono no puede superar los 20 caracteres. ")]
        [Phone]
        public required string PhoneNumber { get; set; }
    }
}