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
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        [ForeignKey("locations")]
        public int LocationId { get; set; }
        public Location Locations { get; set; }

        public ICollection<Booking> Bookings { get; set; }


    }
}