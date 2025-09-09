using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Core.Entities
{
    public class Client(string firstName, string lastName, string email, string phoneNumber)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; } = firstName;
        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; } = lastName;
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; } = email;
        [Required]
        [MaxLength(20)]
        [Phone]
        public required string PhoneNumber { get; set; } = phoneNumber;

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}