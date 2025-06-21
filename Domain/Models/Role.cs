using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public required string Name { get; set; }
        /// <summary>
        /// [] = Regla de estilo al inicializar la colección correspondiente a versiones
        /// Posteriores a C# 9.0
        /// </summary>
        public ICollection<User> Users { get; set; } = [];

        public ICollection<RolePermission> RolePermissions { get; set; } = [];

        public Role(string name)
        {
            Name = name;
        }
        public Role() { }
    }
}