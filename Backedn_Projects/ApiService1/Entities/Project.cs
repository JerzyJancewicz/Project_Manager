namespace ApiService1.Entities
{
    public class Project
    {
        public int IdProject { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public int ProjectDetailsIdProjectDetails { get; set; }
        public ProjectDetails IdProjectDetailsNavigation { get; set; } = default!;
        public ICollection<UserProject>? UserProjects { get; set; }

    }
}
