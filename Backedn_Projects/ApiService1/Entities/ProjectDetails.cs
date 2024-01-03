namespace ApiService1.Entities
{
    public class ProjectDetails
    {
        public int IdProjectDetails { get; set; }
        public string Title { get; set; } = string.Empty;
        public string  Description { get; set; } = string.Empty;
        public int ProjectIdProject { get; set; }
        public int UserIdUser { get; set; }
        public virtual Project IdProjectNavigation { get; set; } = default!;
        public virtual User IdUserNavigation { get; set; } = default!;
    }
}
