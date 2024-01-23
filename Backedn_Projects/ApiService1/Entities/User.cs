using Microsoft.AspNetCore.Identity;

namespace ApiService1.Entities
{
    public class User : IdentityUser
    {
        public int UserIdUser { get; set; }
        public UserDetails IdUserDetailsNavigation { get; set; } = default!;
        public ICollection<UserProject>? UserProjects { get; set; }

    }
}
