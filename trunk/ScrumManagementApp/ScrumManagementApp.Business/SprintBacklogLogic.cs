using System;
using System.Collections.Generic;
using ScrumManagementApp.DAL.Repository;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.Business
{
    /// <summary>
    /// Author: Andrew Moyes
    /// Description: Business logic for Product Backlog. Interacts with DAL and performs any required server side validation
    /// </summary>
    public class SprintBacklogLogic
    {

        private ISprintBacklogRepository _sprintBacklogRepository;
        private IUserStoryRepository _storyRepository; 

        /// <summary>
        /// Constructor. Instantiates the sprint backlog and user story repositories.
        /// </summary>
        public SprintBacklogLogic()
        {
            _sprintBacklogRepository = new SprintBacklogRepository();
            _storyRepository = new UserStoryRepository();
            //_assignedStoryRepository = new AssignedUserStoryRepository();
        }

        /// <summary>
        /// Constructor. Used for unit testing.
        /// </summary>
        /// <param name="SprintBacklogRepo"></param>
        public SprintBacklogLogic(ISprintBacklogRepository SprintBacklogRepo)
        {
            _sprintBacklogRepository = SprintBacklogRepo;
        }

        /// <summary>
        /// Constructor. Used for unit testing.
        /// </summary>
        /// <param name="UserStoryRepo"></param>
        public SprintBacklogLogic(IUserStoryRepository UserStoryRepo)
        {
            _storyRepository = UserStoryRepo;
        }

        /// <summary>
        /// Returns the product backlog for the given project 
        /// </summary>
        /// <param name="SprintID">Primary key of project</param>
        /// <returns></returns>
        public SprintBacklog GetSprintBacklog(Int32 SprintBacklogID)
        {
            return _sprintBacklogRepository.GetSingle(b => b.SprintBacklogId == SprintBacklogID);

        }

        /// <summary>
        /// Returns a list of all the user stories which have been assigned to the given sprint backlog.
        /// </summary>
        /// <param name="SprintBacklogID"></param>
        /// <returns></returns>
        public IList<UserStory> GetSprintBacklogUserStories(Int32 SprintBacklogID)
        {
            return _storyRepository.GetList(us => us.SprintBacklogId == SprintBacklogID);
        }

        /// <summary>
        /// Assigns a user story to the sprint given sprint backlog.
        /// </summary>
        /// <param name="SprintBacklogID"></param>
        /// <param name="story"></param>
        public void AddUserStory(Int32 SprintBacklogID, UserStory story)
        {
            story.SprintBacklogId = SprintBacklogID;
            _storyRepository.Add(story);
        }


        /// <summary>
        /// Deletes / de-assigns a user story from the given sprint backlog.
        /// </summary>
        /// <param name="SprintBacklogID"></param>
        /// <param name="StoryID"></param>
        public void DeleteUserStory(Int32 SprintBacklogID, Int32 StoryID)
        {

            UserStory userStory = _storyRepository.GetSingle(us => us.UserStoryID == StoryID);

            _storyRepository.Remove(userStory);

        }

        /// <summary>
        /// Updates a user story. SprintBacklogID is not required as it will already be contained within the 'story' parameter.
        /// </summary>
        /// <param name="story">An existing user story.</param>
        public void EditUserStory(UserStory story)
        {

            _storyRepository.Update(story);

        }

        /// <summary>
        /// Updates the priority of a user story within a sprint backlog. Does not protect against duplicate priorities.
        /// </summary>
        /// <param name="SprintBacklogID"></param>
        /// <param name="StoryID"></param>
        /// <param name="newPriority"></param>
        public void ReprioritiseUserStory(Int32 SprintBacklogID, Int32 StoryID, Int32 newPriority)
        {
            UserStory userStory = _storyRepository.GetSingle(us => us.UserStoryID == StoryID);
            userStory.Priority = newPriority;
            _storyRepository.Update(userStory);
        }


    }
}
