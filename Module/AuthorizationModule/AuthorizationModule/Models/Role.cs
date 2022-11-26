#nullable disable

namespace AuthorizationModule.Models
{
    public partial class Role: BaseModels
    {
        public int RoleId { get; set; }
        public string RoleKey { get; set; }
        public string RoleDescription { get; set; }
        public int? parentID { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<PivotUserRole> PivotUserRoles { get; set; }
        public virtual ICollection<Role> childRoles { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<PivotRolePermission> PivotRolePermission { get; set; }

        public virtual Role Parent { get; set; }

    }
}
