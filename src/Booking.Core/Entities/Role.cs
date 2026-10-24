namespace Booking.Core.Entities
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        public ICollection<User> Users { get; set; } = [];
        public ICollection<RolePermission> RolePermissions { get; set; } = [];

        public Role(string name)
        {
            Name = name;
        }

        public void AddPermission(int permissionId)
        {
            if (RolePermissions.Any(rp => rp.PermissionId == permissionId)) return;
            RolePermissions.Add(new RolePermission(Id, permissionId));
        }

        public void RemovePermission(int permissionId)
        {
            var permission = RolePermissions.FirstOrDefault(rp => rp.PermissionId == permissionId);
            if (permission != null)
            {
                RolePermissions.Remove(permission);
            }
        }
        public Role() { }
    }
}