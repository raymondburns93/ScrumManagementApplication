using System;
using System.Collections.Generic;
using System.ServiceModel;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.WebService.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProjectService
    {
        // authentication and registration
        [OperationContract]
        ProjectDTO CreateProject(ProjectDTO project, int userId);

        [OperationContract]
        List<ProjectDTO> GetAllProjects();

        [OperationContract]
        List<ProjectDTO> GetProjectsForUser(Int32 UserID);

        [OperationContract]
        UserDTO GetProjectManagerForProject(Int32 projectId );

        [OperationContract]
        UserDTO GetProductOwnerForProject(Int32 ProjectId);

        [OperationContract]
        List<UserDTO> GetScrumMastersForProject(Int32 ProjectId);

        [OperationContract]
        ProjectDTO GetProjectById(Int32 ProjectID);

        [OperationContract]
        ProjectDTO GetProjectByName(String pProjectName);

        [OperationContract]
        bool HasConflictingProjects(Int32 UserID, DateTime Start, DateTime? End);

        [OperationContract]
        void UpdateProject(ProjectDTO projectDTO);

        [OperationContract]
        void AssignProductOwnerToProject(ProjectDTO projectDTO, int userID);

        [OperationContract]
        void AssignsScrumMastersToProject(ProjectDTO projectDTO, int[] userIDs);

        [OperationContract]
        bool IsProjectManager(ProjectDTO projectDTO, int userId);

        [OperationContract]
        bool IsProductOwner(ProjectDTO projectDTO, int userId);

        [OperationContract]
        bool IsScrumMaster(ProjectDTO projectDTO, int userId);

        [OperationContract]
        bool IsDeveloper(ProjectDTO projectDTO, int userId);

    }

}
