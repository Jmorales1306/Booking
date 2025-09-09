using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Core.Entities
{
    public class Location(string name, string address, string description = "")
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; } = name;
        [StringLength(100)]
        public string Address { get; set; } = address;
        public string Description { get; set; } = description;
        public ICollection<Room> Rooms { get; set; } = [];
        
    }
}