using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Core.Entities
{
    public class Permission(string name, string description)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public ICollection<RolePermission> RolePermissions { get; set; } = [];
    }
}