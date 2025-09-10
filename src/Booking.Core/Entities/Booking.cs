namespace Booking.Core.Entities
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public required TimeSpan StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public required TimeSpan EndTime { get; set; }

        [ForeignKey("Room")]
        [Required]
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;

        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [ForeignKey("Client")]
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public Booking(DateTime date, TimeSpan startTime, TimeSpan endTime, int roomId, int userId, int clientId) // ¡Ajustado a TimeSpan!
        {
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            RoomId = roomId;
            UserId = userId;
            ClientId = clientId;
        }

        public Booking() { }
    }
}