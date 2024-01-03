using ApiService1.Context;

namespace ApiService1.Repositories
{

    public interface IProjectRepository
    {

    }
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApiServiceDbContext _context;

        public ProjectRepository(ApiServiceDbContext context)
        {
            _context = context;
        }


    }
}
