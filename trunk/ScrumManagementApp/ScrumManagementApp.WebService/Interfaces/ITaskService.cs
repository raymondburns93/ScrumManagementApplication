using System;
using System.Collections.Generic;
using System.ServiceModel;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.WebService.Interfaces
{
    [ServiceContract]
    public interface ITaskService
    {
        [OperationContract]
        void AddTask(TaskDTO taskDTO);

        [OperationContract]
        IList<TaskDTO> GetTasksForUserStory(Int32 UserStoryID);
    }
}
