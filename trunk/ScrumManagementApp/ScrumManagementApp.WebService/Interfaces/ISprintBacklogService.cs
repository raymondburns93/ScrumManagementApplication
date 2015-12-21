using System;
using System.Collections.Generic;
using System.ServiceModel;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.WebService.Interfaces
{
    /// <summary>
    /// Author: Andrew Moyes
    /// </summary>

    [ServiceContract]
    public interface ISprintBacklogService
    {

        [OperationContract]
        SprintBacklogDTO GetSprintBacklogById(Int32 SprintBacklogID);

        [OperationContract]
        void AddUserStoryToSprintBacklog(Int32 SprintBacklogID, UserStoryDTO userStory);

        [OperationContract]
        List<UserStoryDTO> GetSprintBacklogUserStories(Int32 SprintBacklogID);

        [OperationContract]
        void EditUserStory(Int32 SprintBacklogID, UserStoryDTO userStory);

        [OperationContract]
        void DeleteUserStory(Int32 SprintBacklogID, UserStoryDTO userStory);
    }
}
