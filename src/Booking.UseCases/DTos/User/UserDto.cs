namespace Booking.UseCases.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required int RoleId { get; set; }
    }
}