namespace Booking.UseCases.DTOs.Client
{
    public class ClientUpdateDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        public int Id { get; set; }
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(50, ErrorMessage = "INVALID_LENGTH")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(50, ErrorMessage = "INVALID_LENGTH")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "REQUIRED")]
        [EmailAddress(ErrorMessage = "INVALID_EMAIL")]
        [StringLength(100, ErrorMessage = "INVALID_LENGTH")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "REQUIRED")]
        public required string PhoneNumber { get; set; }

    }
}