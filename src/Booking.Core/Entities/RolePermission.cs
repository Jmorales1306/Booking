using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Core.Entities
{
    public class RolePermission(int roleId, int permissionId)
    {
        [Key]
        [Column(Order = 1)]
        public int RoleId { get; set; } = roleId;
        public Role Role { get; set; } = null!;
        [Key]
        [Column(Order = 2)]
        public int PermissionId { get; set; } = permissionId;
        public Permission Permission { get; set; } = null!;
    }
}