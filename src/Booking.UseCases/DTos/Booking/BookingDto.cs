namespace Booking.UseCases.DTOs.Booking
{
    public class BookingDto
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public required TimeSpan StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public required TimeSpan EndTime { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ClientId { get; set; }

    }
}