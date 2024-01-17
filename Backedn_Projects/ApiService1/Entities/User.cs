using Microsoft.AspNetCore.Identity;

namespace ApiService1.Entities
{
    public class User : IdentityUser
    {
        //public string IdUser { get; set; } = string.Empty;
        //public int RoleIdRole { get; set; }
        public int UserIdUser { get; set; }
        public UserDetails IdUserDetailsNavigation { get; set; } = default!;
        //public Role IdRoleNavigation { get; set; } = default!;
        public ICollection<UserProject>? UserProjects { get; set; }

    }
}
