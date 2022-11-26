#nullable disable

namespace AuthorizationModule.Models
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

    }
}
