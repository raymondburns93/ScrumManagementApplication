using System;
using System.Collections.Generic;
using ScrumManagementApp.Business;
using ScrumManagementApp.Common.DTOs;
using ScrumManagementApp.EntityModels.Models;
using ScrumManagementApp.WebService.Common;
using ScrumManagementApp.WebService.Interfaces;

namespace ScrumManagementApp.WebService.Services
{
    public class SprintBacklogService : ISprintBacklogService
    {
        private SprintBacklogLogic _sprintBacklogLogic = new SprintBacklogLogic();

        public SprintBacklogDTO GetSprintBacklogById(Int32 SprintBacklogID)
        {
            return EntityTranslations.Translate_SprintBacklog_Entity_To_DTO(_sprintBacklogLogic.GetSprintBacklog(SprintBacklogID));
        }

        public void AddUserStoryToSprintBacklog(Int32 SprintBacklogID, UserStoryDTO userStory)
        {
            _sprintBacklogLogic.AddUserStory(SprintBacklogID, EntityTranslations.Translate_UserStory_DTO_To_Entity(userStory));
        }

        public List<UserStoryDTO> GetSprintBacklogUserStories(Int32 SprintBacklogID)
        {
            List<UserStoryDTO> stories = new List<UserStoryDTO>();
            IList<UserStory> entities = _sprintBacklogLogic.GetSprintBacklogUserStories(SprintBacklogID);

            foreach(UserStory us in entities)
            {
                stories.Add(EntityTranslations.Translate_UserStory_Entity_To_DTO(us));
            }
            return stories;
        }

        public void EditUserStory(Int32 SprintBacklogID, UserStoryDTO userStory)
        {
            UserStory us = EntityTranslations.Translate_UserStory_DTO_To_Entity(userStory);
            _sprintBacklogLogic.EditUserStory(us);
        }

        public void DeleteUserStory(Int32 SprintBacklogID, UserStoryDTO userStory)
        {
            _sprintBacklogLogic.DeleteUserStory(SprintBacklogID, userStory.UserStoryID);
        }
    }
}
