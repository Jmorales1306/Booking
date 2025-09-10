namespace Booking.UseCases.DTOs.Room
{
    public class RoomDTo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public required string Name { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000, ErrorMessage = "La capacidad de la sala debe ser un numero mayor de 0")]
        public required int Capacity { get; set; }
        [Required(ErrorMessage = "El ID de la Ubicación es obligatorio.")]
        public required int LocatonId { get; set; }
    }
}