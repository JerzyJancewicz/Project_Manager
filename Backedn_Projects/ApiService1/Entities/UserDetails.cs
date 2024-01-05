namespace ApiService1.Entities
{
    public class UserDetails
    {
        public int IdUserDetails { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public ICollection<User>? Users { get; set; }
    }
}
