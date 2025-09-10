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
        public Role() { }
    }
}