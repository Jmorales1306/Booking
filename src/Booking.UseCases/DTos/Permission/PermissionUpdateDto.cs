namespace Booking.UseCases.DTOs.Permission
{
    public class PermissionUpdateDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(20, ErrorMessage = "INVALID_LENGTH")]
        public required string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}