using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        public required int Capacity { get; set; }
        [Required]
        [ForeignKey("Location")]
        public required int LocationId { get; set; }
        public Location Location { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = [];

        public Room(string name, int capacity, int locationId)
        {
            Name = name;
            Capacity = capacity;
            LocationId = locationId;
        }

        public Room() { }
    }
}