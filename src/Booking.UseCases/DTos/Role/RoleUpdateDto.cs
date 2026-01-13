namespace Booking.UseCases.DTOs.Role
{
    public class RoleUpdateDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        public int Id { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(20, ErrorMessage = "INVALID_LENGTH")]
        public required string Name { get; set; }
    }
}