namespace Booking.UseCases.DTOs.Role
{
    public class RoleInsertDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(20, ErrorMessage = "INVALID_LENGTH")]
        public required string Name { get; set; }
    }
}