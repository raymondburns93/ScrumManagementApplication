using ScrumManagementApp.WebService.Interfaces;
using System;
using System.IO;
using ScrumManagementApp.Business;
using System.Collections.Generic;
using ScrumManagementApp.Common.DTOs;
using ScrumManagementApp.EntityModels.Models;
using ScrumManagementApp.WebService.Common;

namespace ScrumManagementApp.WebService.Services
{
    public class Project_ScrumMasterService : IProjectScrumMasterService
    {
        Project_ScrumMasterLogic logic = new Project_ScrumMasterLogic();

        public Project_ScrumMasterDTO CreateProjectScrumMaster(Project_ScrumMasterDTO project_ScrumMasterDTO)
        {

            string relative = @"..\..\..\ScrumManagementApp.DAL\";
            string absolute = Path.GetFullPath(relative);

            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);

            /*
            using (var db = new ScrumManagementAppContext())
            {
                var user = new User { Username = userDto.Username, Password = userDto.Password };
                db.Users.Add(user);
                db.SaveChanges(); 
            }

            */

            logic.AddProjectScrumMaster(EntityTranslations.Translate_ProjectScrumMaster_DTO_To_Entity(project_ScrumMasterDTO));

            return project_ScrumMasterDTO;

        }

        public List<Project_ScrumMasterDTO> GetAllProjects()
        {
            IList<Project_ScrumMaster> projects = logic.GetAllProjectScrumMasters();
            List<Project_ScrumMasterDTO> Project_ScrumMasterDTOs = new List<Project_ScrumMasterDTO>();

            foreach (Project_ScrumMaster p in projects)
            {
                Project_ScrumMasterDTOs.Add(EntityTranslations.Translate_ProjectScrumMaster_Entity_To_DTO(p));
            }

            projects = null;

            return Project_ScrumMasterDTOs;

        }

        public List<Project_ScrumMasterDTO> GetProjectsByScrumMaster(UserDTO pScrumMaster)
        {
            IList<Project_ScrumMaster> projects = 
                logic.GetProjectsByScrumMaster
                (EntityTranslations.Translate_User_DTO_To_Entity(pScrumMaster));

            List<Project_ScrumMasterDTO> Project_ScrumMasterDTOs = new List<Project_ScrumMasterDTO>();

            foreach (Project_ScrumMaster p in projects)
            {
                Project_ScrumMasterDTOs.Add(EntityTranslations.Translate_ProjectScrumMaster_Entity_To_DTO(p));
            }

            projects = null;

            return Project_ScrumMasterDTOs;

        }

        public List<Project_ScrumMasterDTO> GetScrumMastersByProject(ProjectDTO pProject)
        {
            IList<Project_ScrumMaster> projects = 
                logic.GetScrumMastersByProject
                (EntityTranslations.Translate_Project_DTO_To_Entity(pProject));

            List<Project_ScrumMasterDTO> Project_ScrumMasterDTOs = new List<Project_ScrumMasterDTO>();

            foreach (Project_ScrumMaster p in projects)
            {
                Project_ScrumMasterDTOs.Add(EntityTranslations.Translate_ProjectScrumMaster_Entity_To_DTO(p));
            }

            projects = null;

            return Project_ScrumMasterDTOs;
        }

        public Project_ScrumMasterDTO GetProjectScrumMaster(Int32 pProjectScrumMasterId)
        {

            return EntityTranslations.Translate_ProjectScrumMaster_Entity_To_DTO(logic.GetProjectScrumMaster(pProjectScrumMasterId));
        }
    }
}
