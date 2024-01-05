namespace ApiService1.Entities
{
    public class UserProject
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public User? User { get; set; }
        public Project? Project { get; set; }
    }
}
