namespace ApiService1.DTOs
{
    public class ProjectGET
    {
        public int IdProject { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}
