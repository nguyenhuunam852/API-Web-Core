using API_Web_Core.DTO.User;
using System;
using System.Collections.Generic;

#nullable disable

namespace API_Web_Core.Models
{
    public partial class User:BaseModels
    {
        public User()
        {
            PivotUserRoles = new HashSet<PivotUserRole>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public virtual ICollection<PivotUserRole> PivotUserRoles { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        public static explicit operator User_DTO(User obj)
        {
            User_DTO output = new User_DTO() { userid = obj.UserId, user_name = obj.UserName };
            return output;
        }
    }
}
