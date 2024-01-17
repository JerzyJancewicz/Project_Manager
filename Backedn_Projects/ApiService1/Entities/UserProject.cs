namespace ApiService1.Entities
{
    public class UserProject
    {
        public string UserId { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public User? User { get; set; }
        public Project? Project { get; set; }
    }
}
