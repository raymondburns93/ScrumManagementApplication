using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ScrumManagementApp.EntityModels.Models;
using System.IO;
    
namespace ScrumManagementApp.DAL
{
    public partial class ScrumManagementAppContext : DbContext
    {
        public ScrumManagementAppContext()
            : base("name=DataModel")
        {
            string relative = @"..\..\..\..\ScrumManagementApp.DAL\"; 
            String baseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string absolutePath = Path.GetFullPath(baseDir + relative);
           
            AppDomain.CurrentDomain.SetData("DataDirectory", absolutePath);

            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
 
            //we are using short lived db contexts so these features are no use to us. disabling to reduce overhead.
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer<ScrumManagementAppContext>(new ContextInitialiser());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<UserProject> UserProjects { get; set; }
        public virtual DbSet<Role> roles { get; set; }
        public virtual DbSet<ProductBacklog> ProductBacklogs { get; set; }
        public virtual DbSet<Sprint> Sprints { get; set; }
        public virtual DbSet<UserSprintRole> SprintRoles { get; set; }
        public virtual DbSet<UserStory> UserStories { get; set; }
        public virtual DbSet<SprintBacklog> SprintBacklogs { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
            //One-to-one foreign key associations are not supported by Entity Framework. 
            // one to many Project - ProductBacklog
            modelBuilder.Entity<Project>()
                .HasOptional(x => x.ProductBacklog).WithMany()
                .HasForeignKey(x => x.ProductBacklogId);

            // one to many ProductBacklog - Project
            modelBuilder.Entity<ProductBacklog>()
                .HasRequired(x => x.Project).WithMany()
                .HasForeignKey(x => x.ProjectId);

            //One-to-one foreign key associations are not supported by Entity Framework. 
            // one to many Sprint - SprintBacklog
            modelBuilder.Entity<Sprint>()
                .HasOptional(x => x.SprintBacklog).WithMany()
                .HasForeignKey(x => x.SprintBacklogId);

            // one to many SprintBacklog - Sprint
            modelBuilder.Entity<SprintBacklog>()
                .HasRequired(x => x.sprint).WithMany()
                .HasForeignKey(x => x.SprintID);

            modelBuilder.Entity<Task>().HasOptional(u => u.DeveloperOwnedBy).WithMany().HasForeignKey(u => u.DeveloperOwnedById);

            modelBuilder.Entity<Sprint>().HasRequired(p => p.Project).WithMany(s => s.sprints).WillCascadeOnDelete(false);

        }
    }
}
