namespace Booking.UseCases.DTOs.Location
{
    public class LocationInsertDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(100, ErrorMessage = "INVALID_LENGTH")]
        public required string Name { get; set; }

        [StringLength(255, ErrorMessage = "INVALID_LENGTH")]
        public string Address { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}