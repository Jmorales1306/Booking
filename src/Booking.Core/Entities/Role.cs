using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Core.Entities
{
    public class Role(string name)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; } = name;
        /// <summary>
        /// [] = Regla de estilo al inicializar la colección correspondiente a versiones
        /// Posteriores a C# 9.0
        /// </summary>
        public ICollection<User> Users { get; set; } = [];
        public ICollection<RolePermission> RolePermissions { get; set; } = [];
    }
}