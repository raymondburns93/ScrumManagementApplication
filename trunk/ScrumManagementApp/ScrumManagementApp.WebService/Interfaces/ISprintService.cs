using System.Collections.Generic;
using System.ServiceModel;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.WebService.Interfaces
{
    [ServiceContract]
    public interface ISprintService
    {
        [OperationContract]
        SprintDTO CreateSprint(SprintDTO sprint, int scrumMasterId);

        [OperationContract]
        IList<SprintDTO> GetSprintsForProject(int projectId);

        [OperationContract]
        void AssignDeveloperToSprint(SprintDTO sprintDTO, int userId);

        [OperationContract]
        UserDTO GetScrumManagerForSprint(int sprintId);

        [OperationContract]
        List<UserDTO> GetDevelopersForSprint(int sprintId);

        [OperationContract]
        List<SprintDTO> GetSprintsForUser(int userId);
     

    }
}
