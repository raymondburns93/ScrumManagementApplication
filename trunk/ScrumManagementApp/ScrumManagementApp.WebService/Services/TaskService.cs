using System;
using System.Collections.Generic;
using ScrumManagementApp.Business;
using ScrumManagementApp.Common.DTOs;
using ScrumManagementApp.EntityModels.Models;
using ScrumManagementApp.WebService.Common;
using ScrumManagementApp.WebService.Interfaces;

namespace ScrumManagementApp.WebService.Services
{
    public class TaskService : ITaskService
    {

        private TaskLogic _taskLogic = new TaskLogic();

        public void AddTask(TaskDTO taskDTO)
        {
            Task taskEntity = EntityTranslations.Translate_Task_DTO_To_Entity(taskDTO);
            _taskLogic.Add(taskEntity);
        }

        public void UpdateTask(TaskDTO taskDTO)
        {
            Task Task = EntityTranslations.Translate_Task_DTO_To_Entity(taskDTO);
            _taskLogic.Update(Task);
        }

        public void DeleteTask(Int32 UserStoryID, TaskDTO taskDTO)
        {
            Task Task = EntityTranslations.Translate_Task_DTO_To_Entity(taskDTO);
            _taskLogic.RemoveTask(UserStoryID, Task);
        }
        
        public IList<TaskDTO> GetTasksForUserStory(Int32 UserStoryID)
        {
            IList<Task> tasks = _taskLogic.GetTasksForUserStory(UserStoryID);

            List<TaskDTO> tasksdtos = new List<TaskDTO>();

            foreach (Task t in tasks)
            {
                tasksdtos.Add(EntityTranslations.Translate_Task_Entity_To_DTO(t));
            }

            return tasksdtos;

        }

    }
}
