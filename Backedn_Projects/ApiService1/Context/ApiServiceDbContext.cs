using ApiService1.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiService1.Context
{
    public class ApiServiceDbContext : DbContext
    {
        public ApiServiceDbContext(DbContextOptions<ApiServiceDbContext> options):base(options)
        {
        }

        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectDetails> ProjectDetails { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(e =>
            {
                e.HasKey(p => p.IdProject);
                e.Property(p => p.CreatedAt).IsRequired();
                e.Property(p => p.LastModified).IsRequired();
            });

            modelBuilder.Entity<ProjectDetails>(e =>
            {
                e.HasKey(p => p.IdProjectDetails);
                e.Property(p => p.Title).IsRequired().HasMaxLength(100);
                e.Property(p => p.Description).IsRequired().HasMaxLength(1000);

                e.HasOne(k => k.IdProjectNavigation)
                    .WithMany(k => k.ProjectDetailsList)
                    .HasForeignKey(k => k.ProjectIdProject)
                    .OnDelete(DeleteBehavior.ClientCascade);
                e.HasOne(k => k.IdUserNavigation)
                    .WithMany(k => k.ProjectDetailsList)
                    .HasForeignKey(k => k.UserIdUser)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<Role>(e =>
            {
                e.HasKey(k => k.IdRole);

                e.Property(k => k.RoleName).IsRequired().HasMaxLength(255);
                e.Property(k => k.AssignedBy).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(k => k.IdUser);

                e.Property(k => k.Email).IsRequired().HasMaxLength(255);
                e.Property(k => k.Password).IsRequired().HasMaxLength(255);

                e.HasOne(k => k.IdRoleNavigation)
                    .WithMany(k => k.Users)
                    .HasForeignKey(k => k.RoleIdRole)
                    .OnDelete(DeleteBehavior.ClientCascade);
                e.HasOne(k => k.IdUserDetailsNavigation)
                    .WithMany(k => k.Users)
                    .HasForeignKey(k => k.UserDetailsIdUserDetails)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<UserDetails>(e =>
            {
                e.HasKey(k => k.IdUserDetails);

                e.Property(k => k.Name).IsRequired().HasMaxLength(255);
                e.Property(k => k.Surname).IsRequired().HasMaxLength(255);
            });
        }
    }
}