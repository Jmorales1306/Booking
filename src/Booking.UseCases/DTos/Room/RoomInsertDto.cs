namespace Booking.UseCases.DTOs.Room
{
    public class RoomInsertDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(50, ErrorMessage = "INVALID_LENGTH")]
        public required string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "REQUIRED")]
        [Range(1, 1000, ErrorMessage = "INVALID_RANGE")]
        public required int Capacity { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        public required int LocatonId { get; set; }
    }
}