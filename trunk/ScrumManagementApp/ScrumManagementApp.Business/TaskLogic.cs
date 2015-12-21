using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumManagementApp.DAL.Repository;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.Business
{
    public class TaskLogic
    {

        private TaskRepository _taskRepository;
        private UserStoryRepository _storyRepository;

        /// <summary>
        /// Constructor. Instantiates task and user story repositories.
        /// </summary>
        public TaskLogic()
        {
            _taskRepository = new TaskRepository();
            _storyRepository = new UserStoryRepository();
        }

        /// <summary>
        /// Returns a list of tasks which have been assigned to a user story.
        /// </summary>
        /// <param name="UserStoryID"></param>
        /// <returns></returns>
        public IList<EntityModels.Models.Task> GetTasksForUserStory(Int32 UserStoryID)
        {
            IList<EntityModels.Models.Task> Tasks = new List<EntityModels.Models.Task>();

            Tasks = _taskRepository.GetList(t => t.UserStoryID == UserStoryID);

            return Tasks;
        }

        /// <summary>
        /// Add task
        /// </summary>
        /// <param name="task"></param>
        public void Add(EntityModels.Models.Task task)
        {
            _taskRepository.Add(task);
        }

        /// <summary>
        /// Remove task
        /// </summary>
        /// <param name="task"></param>
        public void Remove(EntityModels.Models.Task task)
        {
            _taskRepository.Remove(task);
        }

        /// <summary>
        /// Update task
        /// </summary>
        /// <param name="task"></param>
        public void Update(EntityModels.Models.Task task)
        {
            _taskRepository.Update(task);
        }

        /// <summary>
        /// Assign a task to a user story
        /// </summary>
        /// <param name="UserStoryID"></param>
        /// <param name="task"></param>
        public void AssignTask(Int32 UserStoryID, EntityModels.Models.Task task)
        {
            UserStory story = _storyRepository.GetSingle(s => s.UserStoryID == UserStoryID);
            if(story != null)
            {
                story.tasks.Add(task);
                _storyRepository.Update(story); 
            }
        }

        /// <summary>
        /// Removes a task from a user story's task collection and from task table
        /// </summary>
        /// <param name="UserStoryID"></param>
        /// <param name="task"></param>
        public void RemoveTask(Int32 UserStoryID, EntityModels.Models.Task task)
        {
            UserStory story = _storyRepository.GetSingle(s => s.UserStoryID == UserStoryID);
            if(story != null)
            {
                story.tasks.Remove(task);
                _storyRepository.Update(story);
                _taskRepository.Remove(task);
            }
        }

    }
}
