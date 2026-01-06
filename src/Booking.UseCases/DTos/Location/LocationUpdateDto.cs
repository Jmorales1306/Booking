namespace Booking.UseCases.DTOs.Location
{
    public class LocationUpdateDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(100, ErrorMessage = "INVALID_LENGTH")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(255, ErrorMessage = "INVALID_LENGTH")]
        public string Address { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}