using ApiService1.Entities;

namespace ApiService1.Services
{

    public interface IProjectService
    {
        public Task<List<Project>> GetAll();
    }

    public class ProjectService : IProjectService
    {
        public Task<List<Project>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
