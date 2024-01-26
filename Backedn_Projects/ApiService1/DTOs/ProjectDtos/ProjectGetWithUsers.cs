namespace ApiService1.DTOs.ProjectDtos
{
    public class ProjectGetWithUsers
    {
        public int IdProject { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime LastModified { get; set; }
        public List<UserGET> users { get; set; } = new List<UserGET>();
    }
}
