using System;
using System.Collections.Generic;
using ScrumManagementApp.Business;
using ScrumManagementApp.Common.DTOs;
using ScrumManagementApp.EntityModels.Models;
using ScrumManagementApp.WebService.Common;
using ScrumManagementApp.WebService.Interfaces;

namespace ScrumManagementApp.WebService.Services
{
    public class ProjectService : IProjectService
    {
        ProjectLogic projectLogic = new ProjectLogic();

        public ProjectDTO CreateProject(ProjectDTO projectDTO, int userId)
        {
            projectLogic.AddProject(EntityTranslations.Translate_Project_DTO_To_Entity(projectDTO), userId);

            return projectDTO;

        }

        public void AssignProductOwnerToProject(ProjectDTO projectDTO, int userId)
        {
            if (projectDTO != null)
            {
                Project p = EntityTranslations.Translate_Project_DTO_To_Entity(projectDTO);

                projectLogic.AssignProductOwner(p, userId);
            }
        }

        public void AssignsScrumMastersToProject(ProjectDTO projectDTO, int[] userIds)
        {
            if (projectDTO != null)
            {
                Project p = EntityTranslations.Translate_Project_DTO_To_Entity(projectDTO);

                projectLogic.AssignScrumMasters(p, userIds);
            }
        }

        public List<ProjectDTO> GetAllProjects()
        {
            IList<Project> projects = projectLogic.GetAllProjects();
            List<ProjectDTO> projectDTOs = new List<ProjectDTO>();

            if (projects != null)
            {
                foreach (Project p in projects)
                {
                    projectDTOs.Add(EntityTranslations.Translate_Project_Entity_To_DTO(p));
                }
            }

            return projectDTOs;
        }

        public bool HasConflictingProjects(Int32 UserID, DateTime Start, DateTime? End)//LMC updated to make end dates nullable
        {
            bool returnValue = false;
            bool endParameterNull = End == null;

            IList<Project> userProjects = projectLogic.GetProjectsForUser(UserID);

            if (userProjects != null)
            {
                // Loop through projects and check for conflicting dates
                foreach (Project p in userProjects)
                {
                    bool existingProjectEndNull = p.EndDate == null;

                    bool StartAfterProjectEnd;
                    bool StartBeforeProjectEnd;
                    if (existingProjectEndNull)
                    {
                        StartAfterProjectEnd = true;
                        StartBeforeProjectEnd = true;
                    }
                    else
                    {
                        Int32 start_end_Diff = DateTime.Compare(Start, (DateTime)p.EndDate);
                        StartAfterProjectEnd = start_end_Diff > 0; // Start after p.end. No confict.
                        StartBeforeProjectEnd = start_end_Diff >= 0; // Start before p.end. Potential conflict.
                    }

                    bool EndAfterProjectStart;
                    if (existingProjectEndNull)
                    {
                        EndAfterProjectStart = true;
                    }
                    else
                    {
                        if(endParameterNull)
                        {
                            EndAfterProjectStart = false;
                        }
                        else
                        {
                            Int32 end_end_Diff = DateTime.Compare((DateTime)End, (DateTime)p.EndDate);
                            EndAfterProjectStart = end_end_Diff >= 0; // End after p.start. Potential conflict.
                        }
                    }

                    // Check if start date of param-Project is after the start date of p
                    bool isConflicted = (StartBeforeProjectEnd && EndAfterProjectStart) || (!StartAfterProjectEnd && EndAfterProjectStart);
                    returnValue = returnValue || isConflicted;
                }
            }

            return returnValue;
        }

        #region old HasConflictingProjects methods in case anyone wants it
        //public bool HasConflictingProjects(Int32 UserID, DateTime Start, DateTime End)
        //{
        //    bool returnValue = false;

        //    IList<Project> userProjects = projectLogic.GetProjectsForUser(UserID);

        //    if (userProjects != null)
        //    {
        //        // Loop through projects and check for conflicting dates
        //        foreach (Project p in userProjects)
        //        {
        //            // Check if start date of param-Project is after the start date of p
        //            Int32 start_end_Diff = DateTime.Compare(Start, p.EndDate);
        //            Int32 start_start_Diff = DateTime.Compare(Start, p.StartDate);
        //            Int32 end_start_Diff = DateTime.Compare(End, p.StartDate);
        //            Int32 end_end_Diff = DateTime.Compare(End, p.EndDate);
        //            bool StartAfterProjectEnd = start_end_Diff > 0; // Start after p.end. No confict.
        //            bool EndBeforeProjectStart = end_end_Diff < 0; // End before p.start. No conflict.
        //            bool StartBeforeProjectEnd = start_end_Diff >= 0; // Start before p.end. Potential conflict.
        //            bool EndAfterProjectStart = end_end_Diff >= 0; // End after p.start. Potential conflict.

        //            bool isConflicted = (StartBeforeProjectEnd && EndAfterProjectStart) || (!StartAfterProjectEnd && EndAfterProjectStart);
        //            returnValue = returnValue || isConflicted;
        //        }
        //    }

        //    return returnValue;
        //}
        #endregion

        public List<ProjectDTO> GetProjectsForUser(Int32 UserID)
        {
            IList<Project> projects = projectLogic.GetProjectsForUser(UserID);
            List<ProjectDTO> projectsdtos = new List<ProjectDTO>();

            if (projects != null)//Projects exist
            {
                foreach (Project p in projects)
                {
                    projectsdtos.Add(EntityTranslations.Translate_Project_Entity_To_DTO(p));
                }
            }
            return projectsdtos;


        }

        public UserDTO GetProjectManagerForProject(Int32 projectId)
        {
            User projectManager = projectLogic.GetProjectManager(projectId);

            if( projectManager != null)
            {
                  return EntityTranslations.Translate_User_Entity_To_DTO(projectManager);
            }
            return null;

        }

        public UserDTO GetProductOwnerForProject(Int32 ProjectId)
        {
            User productOwner = projectLogic.GetProductOwner(ProjectId);

            if (productOwner != null)
            {
                return EntityTranslations.Translate_User_Entity_To_DTO(productOwner);
            }
            return null;

        }

        public List<UserDTO> GetScrumMastersForProject(Int32 ProjectId)
        {
            IList<User> scrumMasters = projectLogic.GetScrumMasters(ProjectId);

            List<UserDTO> scrumMastersDTO = new List<UserDTO>();

            foreach (User scrumMaster in scrumMasters)
            {
                scrumMastersDTO.Add(EntityTranslations.Translate_User_Entity_To_DTO(scrumMaster));

            }
            return scrumMastersDTO;
        }

        public ProjectDTO GetProjectById(Int32 ProjectID)
        {
            Project project = projectLogic.GetProject(ProjectID);
            ProjectDTO projectDTO = null;

            if (project != null)
            {
                projectDTO = EntityTranslations.Translate_Project_Entity_To_DTO(project);
            }

            return projectDTO;

        }

        public ProjectDTO GetProjectByName(String pProjectName)
        {
            Project p = projectLogic.GetProjectByName(pProjectName);
            if (p != null)
                return EntityTranslations.Translate_Project_Entity_To_DTO(projectLogic.GetProjectByName(pProjectName));
            return null;
        }

        public void UpdateProject(ProjectDTO projectDTO)
        {
            if (projectDTO != null)
            {
                Project toSave = EntityTranslations.Translate_Project_DTO_To_Entity(projectDTO);
                projectLogic.UpdateProject(toSave);
            }
        }


        public bool IsProjectManager(ProjectDTO projectDTO, int userId)
        {
            if (projectDTO == null)
            {
                return false;
            }
            return projectLogic.IsProjectManager(EntityTranslations.Translate_Project_DTO_To_Entity(projectDTO), userId);
        }

        public bool IsProductOwner(ProjectDTO projectDTO, int userId)
        {
            if (projectDTO == null)
            {
                return false;
            }
            return projectLogic.IsProductOwner(EntityTranslations.Translate_Project_DTO_To_Entity(projectDTO), userId);
        }

        public bool IsScrumMaster(ProjectDTO projectDTO, int userId)
        {
            if (projectDTO == null)
            {
                return false;
            }
            return projectLogic.IsScrumMaster(EntityTranslations.Translate_Project_DTO_To_Entity(projectDTO), userId);
        }

        public bool IsDeveloper(ProjectDTO projectDTO, int userId)
        {
            if (projectDTO == null)
            {
                return false;
            }
            return projectLogic.IsDeveloper(EntityTranslations.Translate_Project_DTO_To_Entity(projectDTO), userId);
        }
    }
}
