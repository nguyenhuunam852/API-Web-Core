using System;
using System.Collections.Generic;

#nullable disable

namespace API_Web_Core.Models
{
    public partial class Role
    {
        public Role()
        {
            PivotUserRoles = new HashSet<PivotUserRole>();
        }

        public int RoleId { get; set; }
        public string RoleKey { get; set; }
        public string? RoleDescription { get; set; }
        public int? parentID { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<PivotUserRole> PivotUserRoles { get; set; }
        public virtual ICollection<Role> childRoles { get; set; }
        public virtual Role Parent { get; set; }

    }
}
