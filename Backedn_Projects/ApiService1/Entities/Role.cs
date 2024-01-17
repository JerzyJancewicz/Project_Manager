using Microsoft.AspNetCore.Identity;

namespace ApiService1.Entities
{
    public class Role : IdentityRole
    {
        //public string IdRole { get; set; } = string.Empty;
        //public string RoleName { get; set; } = string.Empty;
        public string AssignedBy { get; set; } = string.Empty;
        //public ICollection<User>? Users { get; set; }
    }
}
