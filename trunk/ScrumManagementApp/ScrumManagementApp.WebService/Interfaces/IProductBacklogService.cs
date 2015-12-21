using System.Collections.Generic;
using System.ServiceModel;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.WebService.Interfaces
{
    /// <summary>
    /// Author: Andrew Baird
    /// </summary>

    [ServiceContract]
    public interface IProductBacklogService
    {

        [OperationContract]
        ProductBacklogDTO GetProductBacklogByProjectId(int projectId);

        [OperationContract]
        void AddUserStoryToProjectBacklog(int projectID, UserStoryDTO userStory);

        [OperationContract]
        List<UserStoryDTO> GetProductBacklogUserStories(int projectId);

        [OperationContract]
        void EditUserStory(int projectId, UserStoryDTO userStory);

        [OperationContract]
        void DeleteUserStory(int projectId, UserStoryDTO userStory);

        [OperationContract]
        void ReprioritiseUserStory(int projectId, UserStoryDTO userStory);

    }
}
