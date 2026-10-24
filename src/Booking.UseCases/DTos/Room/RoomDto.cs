namespace Booking.UseCases.DTOs.Room
{
    public class RoomDTo
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required int Capacity { get; set; }
        public required int LocatonId { get; set; }
    }
}