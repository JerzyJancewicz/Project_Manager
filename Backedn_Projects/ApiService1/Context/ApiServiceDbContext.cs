using ApiService1.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiService1.Context
{
    public class ApiServiceDbContext : IdentityDbContext<User, Role, string>
    {
        public ApiServiceDbContext(DbContextOptions<ApiServiceDbContext> options):base(options)
        {
        }

        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectDetails> ProjectDetails { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserProject> UserProject { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.IdUserDetailsNavigation)
                .WithMany(ud => ud.Users)
                .HasForeignKey(u => u.UserIdUser);

            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId });

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId);

            modelBuilder.Entity<Project>()
                .HasKey(e => e.IdProject);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.IdProjectDetailsNavigation)
                .WithMany(pd => pd.Projects)
                .HasForeignKey(p => p.ProjectDetailsIdProjectDetails);

            modelBuilder.Entity<ProjectDetails>()
                .HasKey(e => e.IdProjectDetails);

            modelBuilder.Entity<UserDetails>()
                .HasKey(e => e.IdUserDetails);
        }
    }
}