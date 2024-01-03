namespace ApiService1.Entities
{
    public class Project
    {
        public int IdProject { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public virtual ICollection<ProjectDetails> ProjectDetailsList { get; set; } = new List<ProjectDetails>();
    }
}
