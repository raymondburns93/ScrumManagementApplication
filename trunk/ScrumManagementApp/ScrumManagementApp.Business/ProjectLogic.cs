using ScrumManagementApp.DAL.Repository;
using System;
using System.Collections.Generic;
using ScrumManagementApp.EntityModels.Models;
using System.Linq;

namespace ScrumManagementApp.Business
{
    public class ProjectLogic
    {
        private readonly IProjectRepository repository;
        private readonly IProductBacklogRepository productBacklogRepository;
        private readonly IUserProjectRepository uprRepository = new UserProjectRepository();

        /// <summary>
        /// Constructor. Instantiates project and product backlog repositories.
        /// </summary>
        public ProjectLogic()
        {
            repository = new ProjectRepository();
            productBacklogRepository = new ProductBacklogRepository();
        }

        /// <summary>
        /// Constructor. Used for unit testing.
        /// </summary>
        /// <param name="pRepository"></param>
        public ProjectLogic(IProjectRepository pRepository)
        {
            repository = pRepository;
        }

        /// <summary>
        /// Returns a list of all known projects.
        /// </summary>
        /// <returns></returns>
        public IList<Project> GetAllProjects()
        {
            return repository.GetAll(null);
        }

        //public Project GetCurrentProjectForProductOwner(int pProductOwnerUserId, DateTime pStart, DateTime pEnd)
        //{
        //    return repository.GetSingle(p => p.ProjectOwner.Equals(pProductOwnerUserId) && p.StartDate <= pStart && p.EndDate >= pEnd, null);
        //}

        /// <summary>
        /// Returns a project based on the given project id.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public Project GetProject(Int32 projectID)
        {
            return repository.GetSingle(p => p.ProjectId.Equals(projectID), null);
        }

        /// <summary>
        /// Returns a project based on the given project name.
        /// </summary>
        /// <param name="pProjectName"></param>
        /// <returns></returns>
        public Project GetProjectByName(String pProjectName)
        {
            return repository.GetSingle(p => p.ProjectName.Equals(pProjectName.Trim()), null);
        }

        /// <summary>
        /// Creates a new project from an entity and assigns the passed user id as the project manager.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="userId"></param>
        public void AddProject(Project project, int userId)
        {
            //check project with same name does not exist already
            if (GetProjectByName(project.ProjectName) == null)
            {
                //project.ProjectManagerId = project.ProjectManager.UserId;
                //project.ProjectManager = null;
                repository.Add(project,userId);
            }

          //  Project project1 = GetProjectByName(project.ProjectName);

//            ProductBacklog pb = new ProductBacklog();
  //          pb.ProjectId = project1.ProjectId;

    //        productBacklogRepository.Add(pb);

      //      project1.ProductBacklogId = productBacklogRepository.GetSingle(p => p.ProjectId == project1.ProjectId).ProductBacklogId;
        //    UpdateProject(project1);



          //  repository.Add(project);

            //UserProject userproj = new UserProject();
            //userproj.project = project;
            //userproj.user.UserId = 1;,UserIds,2
            //userproj.role = 


        }

        /// <summary>
        /// Updates a project.
        /// </summary>
        /// <param name="project"></param>
        public void UpdateProject(Project project)
        {
            repository.Update(project);
        }

        /// <summary>
        /// Removes a project from the database completely.
        /// </summary>
        /// <param name="project"></param>
        public void RemoveProject(Project project)
        {
            repository.Remove(project);
        }

        /// <summary>
        /// Returns all projects of which the given user is involved.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Project> GetProjectsForUser(int userId)
        {
            IList<Project> toReturn = repository.GetProjectListForUser(userId).OrderByDescending(p => p.ProjectName).ToList();
            return toReturn;
        }

        /// <summary>
        /// Updates the product owner of a project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="userId"></param>
        public void AssignProductOwner(Project project, int userId)
        {
          
            repository.AssignRole(project,RoleType.ProductOwner,userId);
            
        }

        /// <summary>
        /// Assigns the scrum masters of a project. Receives a list of integers which correspond to the user ids of the scrum masters.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="userIds"></param>
        public void AssignScrumMasters(Project project, int[] userIds)
        {

            repository.AssignRole(project,RoleType.ScrumMaster,userIds);
        }

        /// <summary>
        /// Simply checks if the given user is the project manager of the given project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsProjectManager(Project project, int userId)
        {
            return repository.GetProjectRole(project, userId, RoleType.ProjectManager);
        }

        /// <summary>
        /// Checks if the given user is the product owner of the given project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsProductOwner(Project project, int userId)
        {
            return repository.GetProjectRole(project, userId, RoleType.ProductOwner);
        }

        /// <summary>
        /// Checks if the given user is the scrum master of the given project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsScrumMaster(Project project, int userId)
        {
            return repository.GetProjectRole(project, userId, RoleType.ScrumMaster);
        }

        /// <summary>
        /// Checks if the given user is an assigned-developer on the given project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsDeveloper(Project project, int userId)
        {
            return repository.GetProjectRole(project, userId, RoleType.Developer);
        }

        /// <summary>
        /// Returns a user entity of the assigned project manager of the given project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public User GetProjectManager(int projectId)
        {
            UserProject userProjectRole = uprRepository.GetSingle(p => p.ProjectId == projectId && p.role.RoleType == RoleType.ProjectManager, u => u.user, r => r.role);
            if (userProjectRole != null)
            {
                return userProjectRole.user;
            }
            return null;   
        }

        /// <summary>
        /// Returns a user entity of the assigned product owner of the given project.
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public User GetProductOwner(int ProjectId)
        {

            UserProject userProjectRole = uprRepository.GetSingle(p => p.ProjectId == ProjectId && p.role.RoleType == RoleType.ProductOwner, u => u.user, r => r.role);
            if (userProjectRole != null)
            {
                return userProjectRole.user;
            }
            return null; 
        
        }

        /// <summary>
        /// Returns a list of user entites where each user is an assigned scrum master for the given project.
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public IList<User> GetScrumMasters(int ProjectId)
        {
            IList<UserProject> userProjectRole = uprRepository.GetList(p => p.ProjectId == ProjectId && p.role.RoleType == RoleType.ScrumMaster, u => u.user, r => r.role);
            
            if (userProjectRole != null)
            {
                return userProjectRole.Select(u => u.user).ToList();
            }
            return null; 
        }
    }
}
