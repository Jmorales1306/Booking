using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = [];

        public User(string firstName, string lastName, string email, int roleId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            RoleId = roleId;
        }

        public User() { }
    }
}