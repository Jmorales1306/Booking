namespace Booking.UseCases.DTOs.Booking
{
    public class BookingUpdateDto
    {
        [Required(ErrorMessage = "REQUIRED")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime Date { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [Range(typeof(TimeSpan), "00:00:00", "23:59:59", ErrorMessage = "INVALID_TIME_RANGE")]
        [DataType(DataType.Time)]
        public required TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [Range(typeof(TimeSpan), "00:00:00", "23:59:59", ErrorMessage = "INVALID_TIME_RANGE")]
        [DataType(DataType.Time)]
        public required TimeSpan EndTime { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        public required int RoomId { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        public required int UserId { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        public required int ClientId { get; set; }

    }
}