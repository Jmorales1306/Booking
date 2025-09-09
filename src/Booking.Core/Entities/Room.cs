using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Core.Entities
{
    public class Room(string name, int capacity, int locationId)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; } = name;
        [Required]
        public required int Capacity { get; set; } = capacity;
        [Required]
        [ForeignKey("Location")]
        public required int LocationId { get; set; } = locationId;
        public Location Location { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = [];
    }
}