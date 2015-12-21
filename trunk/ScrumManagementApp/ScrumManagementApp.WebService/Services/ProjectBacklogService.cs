using System.Collections.Generic;
using ScrumManagementApp.Business;
using ScrumManagementApp.Common.DTOs;
using ScrumManagementApp.EntityModels.Models;
using ScrumManagementApp.WebService.Common;
using ScrumManagementApp.WebService.Interfaces;

namespace ScrumManagementApp.WebService.Services
{
    /// <summary>
    /// Author: Andrew Baird
    /// </summary>
    public class ProductBacklogService : IProductBacklogService
    {
        ProductBacklogLogic productBacklogLogic = new ProductBacklogLogic();

        public ProductBacklogDTO GetProductBacklogByProjectId(int projectId)
        {
            ProductBacklogDTO pbDTO = new ProductBacklogDTO();
            ProductBacklog productBacklog = productBacklogLogic.GetProductBacklog(projectId);

            if (productBacklog != null)
            {
                pbDTO = EntityTranslations.Translate_ProductBacklog_Entity_To_DTO(productBacklog);
            }

            return pbDTO;
        }

        public List<UserStoryDTO> GetProductBacklogUserStories(int projectId)
        {
            IList<UserStory> userStories = productBacklogLogic.GetProductBacklogUserStories(projectId);
            List<UserStoryDTO> userStoryDTO = new List<UserStoryDTO>();

            if (userStories != null)
            {
                foreach (UserStory u in userStories)
                {
                    userStoryDTO.Add(EntityTranslations.Translate_UserStory_Entity_To_DTO(u));
                }
            }

            return userStoryDTO;
        }

        public void AddUserStoryToProjectBacklog(int projectID, UserStoryDTO userStory)
        {

            UserStory us = EntityTranslations.Translate_UserStory_DTO_To_Entity(userStory);

            productBacklogLogic.AddUserStory(projectID, us);
        }

        public void EditUserStory(int projectId, UserStoryDTO userStory)
        {
            UserStory us = EntityTranslations.Translate_UserStory_DTO_To_Entity(userStory);

            productBacklogLogic.EditUserStory(projectId, us);
        }

        public void DeleteUserStory(int projectId, UserStoryDTO userStory)
        {
            UserStory us = EntityTranslations.Translate_UserStory_DTO_To_Entity(userStory);
            productBacklogLogic.DeleteUserStory(projectId, us.UserStoryID);
        }

        //basically the same as edituserstory (atleast for now) - is this method needed?
        public void ReprioritiseUserStory(int projectId, UserStoryDTO userStory)
        {
            EditUserStory(projectId, userStory);
        }

    }
}
