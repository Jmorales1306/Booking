namespace Booking.UseCases.DTOs.User
{
    public class UserInsertDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(50, ErrorMessage = "INVALID_LENGTH")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(50, ErrorMessage = "INVALID_LENGTH")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(100, ErrorMessage = "INVALID_LENGTH")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        public required int RoleId { get; set; }
    }
}