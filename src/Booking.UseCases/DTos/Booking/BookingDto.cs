namespace Booking.UseCases.DTOs.Booking
{
    public class BookingDto
    {
        public int Id { get; set; }
        public required DateTime Date { get; set; }
        public required TimeSpan StartTime { get; set; }
        public required TimeSpan EndTime { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
    }
}