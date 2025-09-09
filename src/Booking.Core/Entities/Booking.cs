using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Core.Entities
{
    public class Booking(DateTime date, TimeSpan startTime, TimeSpan endTime, int roomId, int userId, int clientId)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime Date { get; set; } = date;

        [Required]
        [DataType(DataType.Time)]
        public required TimeSpan StartTime { get; set; } = startTime;

        [Required]
        [DataType(DataType.Time)]
        public required TimeSpan EndTime { get; set; } = endTime;

        [ForeignKey("Room")]
        [Required]
        public int RoomId { get; set; } = roomId;
        public Room Room { get; set; } = null!;

        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; } = userId;
        public User User { get; set; } = null!;

        [ForeignKey("Client")]
        [Required]
        public int ClientId { get; set; } = clientId;
        public Client Client { get; set; } = null!;
    }
}