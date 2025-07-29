using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Booking
{
    public class BookingUpdateDto
    {
        [Required(ErrorMessage = "El Id de la reserva es requerido para actualizar.")]
        public required int Id { get; set; }

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