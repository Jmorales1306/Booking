namespace Booking.Core.Entities
{
    public class Client
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
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }
        [Required]
        [MaxLength(20)]
        [Phone]
        public required string PhoneNumber { get; set; }

        public ICollection<Booking> Bookings { get; set; } = [];

        public Client(string firstName, string lastName, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        
        public Client() { }
    }
}