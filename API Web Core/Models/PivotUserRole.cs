using System;
using System.Collections.Generic;

#nullable disable

namespace API_Web_Core.Models
{
    public partial class PivotUserRole
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
