using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Location
{
    public class LocationUpdateDto
    {
        [Required(ErrorMessage = "El Id de la ubicacion es requerido para actualizar.")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "Se debe agregar un nombre a la ubicacion.")]
        [StringLength(100, ErrorMessage = "El nombre de la ubicacion no puede superar los 100 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La direccion de la ubicacion es requerida.")]
        [StringLength(255, ErrorMessage = "La direccion no puede superar los 255 caracteres")]
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}