namespace ApiService1.Entities
{
    public class ProjectDetails
    {
        public int IdProjectDetails { get; set; }
        public string Title { get; set; } = string.Empty;
        public string  Description { get; set; } = string.Empty;
        public ICollection<Project>? Projects { get; set; }
    }
}
