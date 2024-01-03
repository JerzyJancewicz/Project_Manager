namespace ApiService1.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleIdRole { get; set; }
        public int UserDetailsIdUserDetails { get; set; }
        public virtual ICollection<ProjectDetails> ProjectDetailsList { get; set; } = new List<ProjectDetails>();
        public virtual UserDetails IdUserDetailsNavigation { get; set; } = default!;
        public virtual Role IdRoleNavigation { get; set; } = default!;

    }
}
