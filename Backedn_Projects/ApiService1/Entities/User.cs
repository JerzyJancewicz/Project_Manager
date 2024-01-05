namespace ApiService1.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleIdRole { get; set; }
        public int UserIdUser { get; set; }
        public UserDetails IdUserDetailsNavigation { get; set; } = default!;
        public Role IdRoleNavigation { get; set; } = default!;
        public ICollection<UserProject>? UserProjects { get; set; }

    }
}
