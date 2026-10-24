namespace Booking.UseCases.DTOs.Booking
{
    public class BookingInsertDto
    {
        [Required(ErrorMessage = "La Fecha es requerida para realizar la reserva.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime Date { get; set; }

        [Required(ErrorMessage = "La hora de inicio es requerida para realizar la reserva.")]
        [DataType(DataType.Time)]
        public required TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "La hora de fin es requerida para realizar la reserva.")]
        [DataType(DataType.Time)]
        public required TimeSpan EndTime { get; set; }

        [Required(ErrorMessage = "El Id de la sala es requerida para realizar la reserva.")]
        public required int RoomId { get; set; }

        [Required(ErrorMessage = "El Id del usuario es requerido para realizar la reserva.")]
        public required int UserId { get; set; }

        [Required(ErrorMessage = "El Id del cliente es requerido para realizar la reserva.")]
        public required int ClientId { get; set; }
    }
}