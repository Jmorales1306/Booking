namespace Booking.UseCases.DTOs.Location
{
    public class LocationInsertDto
    {
        [Required(ErrorMessage = "Se debe agregar un nombre a la ubicacion.")]
        [StringLength(100, ErrorMessage = "El nombre de la ubicacion no puede superar los 100 caracteres.")]
        public required string Name { get; set; }
        [StringLength(255, ErrorMessage = "La direccion no puede superar los 255 caracteres")]
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}