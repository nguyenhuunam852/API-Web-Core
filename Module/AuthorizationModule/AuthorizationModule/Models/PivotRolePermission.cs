﻿#nullable disable

namespace AuthorizationModule.Models
{
    public class PivotRolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
