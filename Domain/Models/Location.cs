using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [StringLength(100)]
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Room> Rooms { get; set; } = [];

        public Location(string name, string address = "", string description = "")
        {
            Name = name;
            Address = address;
            Description = description;
        }

        public Location() { }
    }
}