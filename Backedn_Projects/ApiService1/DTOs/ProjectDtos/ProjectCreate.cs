namespace ApiService1.DTOs
{
    public class ProjectCreate
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<UserGET> users { get; set; } = new List<UserGET>();
    }
}
