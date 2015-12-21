using ScrumManagementApp.DAL.Repository;
using System;
using System.Collections.Generic;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.Business
{
    public class Project_ScrumMasterLogic
    {
        private readonly IProject_ScrumMasterRepository repository;

        public Project_ScrumMasterLogic()
        {
            repository = new Project_ScrumMasterRepository();
        }

        public IList<Project_ScrumMaster> GetAllProjectScrumMasters()
        {
            return repository.GetAll(null);
        }

        public Project_ScrumMaster GetProjectScrumMaster(Int32 pProjectScrumMasterId)
        {
            return repository.GetSingle(p => p.ProjectScrumMasterId.Equals(pProjectScrumMasterId), null);
        }

        public IList<Project_ScrumMaster> GetScrumMastersByProject(Project pProject)
        {
            return repository.GetAll(p => p.Project.Equals(pProject), null);
        }

        public IList<Project_ScrumMaster> GetProjectsByScrumMaster(User pScrumMaster)
        {
            return repository.GetAll(p => p.ScrumMaster.Equals(pScrumMaster), null);
        }

        public void AddProjectScrumMaster(Project_ScrumMaster project)
        {
            repository.Add(project);
        }

        public void UpdateProjectScrumMaster(Project_ScrumMaster project)
        {
            repository.Update(project);
        }

        public void RemoveProjectScrumMaster(Project_ScrumMaster project)
        {
            repository.Remove(project);
        }

    }
}
