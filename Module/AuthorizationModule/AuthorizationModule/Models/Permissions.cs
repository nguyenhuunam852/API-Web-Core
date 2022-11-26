using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AuthorizationModule.Models
{
    public class Permission: BaseModels
    {
        [Key]
        public int PermissionId { get; set; }
        [Required]
        public string PermissionKey { get; set; }
        public string PermissionDescription { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<PivotRolePermission> PivotRolePermission { get; set; }
    }
}
