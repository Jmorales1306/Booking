using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class RolePermission
    {
        [Key]
        [Column(Order = 1)]
        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;

        [Key]
        [Column(Order = 2)]
        public int PermissionId { get; set; }

        public Permission Permission { get; set; } = null!;

        public RolePermission(int roleId, int permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
        public RolePermission() { }

    }
}