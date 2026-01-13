namespace Booking.UseCases.DTOs.Permission
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}