using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Core.Entities
{
    public class User(string firstName, string lastName, string email, int roleId)
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
        [MaxLength(100)]
        [EmailAddress]
        public required string Email { get; set; } = email;

        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; } = roleId;
        public Role Role { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = [];

    }
}