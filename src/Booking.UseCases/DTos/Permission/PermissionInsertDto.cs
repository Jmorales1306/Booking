namespace Booking.UseCases.DTOs.Permission
{
    public class PermissionInsertDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(20, ErrorMessage = "INVALID_LENGTH")]
        public required string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}