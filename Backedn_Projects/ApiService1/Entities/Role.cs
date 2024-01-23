using Microsoft.AspNetCore.Identity;

namespace ApiService1.Entities
{
    public class Role : IdentityRole
    {
        public string AssignedBy { get; set; } = string.Empty;
    }
}
