namespace Booking.UseCases.DTOs.Location
{
    public class LocationDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}