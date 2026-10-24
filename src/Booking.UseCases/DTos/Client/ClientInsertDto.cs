namespace Booking.UseCases.DTOs.Client
{
    public class ClientInsertDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(50, ErrorMessage = "INVALID_LENGTH")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(50, ErrorMessage = "INVALID_LENGTH")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(100, ErrorMessage = "INVALID_LENGTH")]
        [EmailAddress(ErrorMessage = "INVALID_EMAIL")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "REQUIRED")]
        public required string PhoneNumber { get; set; }
    }
}