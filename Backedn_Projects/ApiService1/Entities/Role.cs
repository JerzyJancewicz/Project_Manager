namespace ApiService1.Entities
{
    public class Role
    {
        public int IdRole { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string AssignedBy { get; set; } = string.Empty;
        public ICollection<User>? Users { get; set; }
    }
}
