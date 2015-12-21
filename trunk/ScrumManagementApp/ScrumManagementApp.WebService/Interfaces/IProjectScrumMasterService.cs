using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.WebService.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProjectScrumMasterService
    {
        // authentication and registration
        [OperationContract]
        Project_ScrumMasterDTO CreateProjectScrumMaster(Project_ScrumMasterDTO project_ScrumMasterDTO);

        [OperationContract]
        List<Project_ScrumMasterDTO> GetAllProjects();

        [OperationContract]
        List<Project_ScrumMasterDTO> GetProjectsByScrumMaster(UserDTO pScrumMaster);

        [OperationContract]
        List<Project_ScrumMasterDTO> GetScrumMastersByProject(ProjectDTO pProject);

        [OperationContract]
        Project_ScrumMasterDTO GetProjectScrumMaster(Int32 pProjectScrumMasterId);

    }

}
