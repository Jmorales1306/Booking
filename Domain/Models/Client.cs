using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string FristName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        [Phone]
        public required string PhoneNumber { get; set; } = string.Empty;
        public ICollection<Booking> Bookings { get; set; } = [];

        public Client(string firstName, string lastName, string email, string phoneNumber)
        {
            FristName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Client() { }
    }
}