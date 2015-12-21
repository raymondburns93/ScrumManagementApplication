using ScrumManagementApp.Common.DTOs;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.WebService.Common
{
    public class EntityTranslations
    {
        #region Projects
        public static Project Translate_Project_DTO_To_Entity(ProjectDTO dto)
        {
            Project project = new Project
            {
                ProjectId = dto.ProjectId,
                ProjectName = dto.ProjectName,
                ProjectDescription = dto.ProjectDescription,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };
            return project;
        }

        public static ProjectDTO Translate_Project_Entity_To_DTO(Project entity)
        {
            ProjectDTO dto = new ProjectDTO
            {
                ProjectId = entity.ProjectId,
                ProjectName = entity.ProjectName,
                ProjectDescription = entity.ProjectDescription,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
            return dto;
        }

        #endregion

        #region Users

        public static UserDTO Translate_User_Entity_To_DTO(User user)
        {
            UserDTO userDto = new UserDTO();
            userDto.UserId = user.UserId;
            userDto.Email = user.Email;
            userDto.Password = user.Password;
            userDto.Alias = user.Alias;
            userDto.Firstname = user.Firstname;
            userDto.Lastname = user.Lastname;
            user.IsProductOwner = user.IsProductOwner;
            user.IsScrumMaster = user.IsScrumMaster;
            user.IsDeveloper = user.IsDeveloper;
            return userDto;
        }

        public static User Translate_User_DTO_To_Entity(UserDTO user)
        {
            User entity = new User
            {
                UserId = user.UserId,
                Alias = user.Alias,
                Email = user.Email,
                Password = user.Password,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                IsDeveloper = user.IsDeveloper,
                IsProductOwner = user.IsProductOwner,
                IsScrumMaster = user.IsScrumMaster
            };
            return entity;
        }

        #endregion

        #region ProductBacklog
        public static ProductBacklog Translate_ProductBacklog_DTO_To_Entity(ProductBacklogDTO dto)
        {
            ProductBacklog productBacklog = new ProductBacklog
            {
                ProductBacklogId = dto.ProductBacklogId,
                BacklogTitle = dto.BacklogTitle,
                ProductOwnerID = dto.ProductOwnerID
            };
            return productBacklog;
        }

        public static ProductBacklogDTO Translate_ProductBacklog_Entity_To_DTO(ProductBacklog entity)
        {
            ProductBacklogDTO dto = new ProductBacklogDTO
            {
                ProductBacklogId = entity.ProductBacklogId,
                BacklogTitle = entity.BacklogTitle,
                ProjectID = entity.ProjectId,
                ProductOwnerID = entity.ProductOwnerID
            };
            return dto;
        }

        #endregion

        #region SprintBacklog

        public static SprintBacklog Translate_SprintBacklog_DTO_To_Entity(SprintBacklogDTO dto)
        {
            SprintBacklog sprintBacklog = new SprintBacklog
            {
                SprintBacklogId = dto.SprintBacklogID,
                BacklogTitle = dto.Title,
                SprintID = dto.SprintID
            };
            return sprintBacklog;
        }

        public static SprintBacklogDTO Translate_SprintBacklog_Entity_To_DTO(SprintBacklog entity)
        {
            SprintBacklogDTO dto = new SprintBacklogDTO
            {
                SprintBacklogID = entity.SprintBacklogId,
                Title = entity.BacklogTitle,
                SprintID = entity.SprintID
            };
            return dto;
        }


        #endregion

        #region UserStory
        public static UserStory Translate_UserStory_DTO_To_Entity(UserStoryDTO dto)
        {
            UserStory userStory = new UserStory
            {
                UserStoryID = dto.UserStoryID,
                Description = dto.Description,
                Locked = dto.Locked,
                Priority = dto.Priority,
                ProductBacklogId = dto.ProductBacklogId

            };
            return userStory;
        }

        public static UserStoryDTO Translate_UserStory_Entity_To_DTO(UserStory entity)
        {
            UserStoryDTO dto = new UserStoryDTO
            {
                UserStoryID = entity.UserStoryID,
                Description = entity.Description,
                Locked = entity.Locked,
                Priority = entity.Priority,
                ProductBacklogId = entity.ProductBacklogId

            };
            return dto;
        }

        #endregion

        #region Sprint
        public static Sprint Translate_Sprint_DTO_To_Entity(SprintDTO dto)
        {
            Sprint sprint = new Sprint
            {
                SprintID = dto.SprintId,
                SprintName = dto.SprintName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ProjectId = dto.ProjectId
                
            };
            return sprint;
        }

        public static SprintDTO Translate_Sprint_Entity_To_DTO(Sprint entity)
        {
            SprintDTO dto = new SprintDTO
            {
                SprintId = entity.SprintID,
                SprintName = entity.SprintName,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                ProjectId = entity.ProjectId,
                SprintBacklogId = entity.SprintBacklogId
               
            };
            return dto;
        }

        #endregion

        #region Task
        public static Task Translate_Task_DTO_To_Entity(TaskDTO dto)
        {
            Task task = new Task
            {
                TaskId = dto.TaskId,
                TaskName = dto.TaskName,
                TaskDescription = dto.TaskDescription,
                HoursRemaining = dto.HoursRemaining,
                DeveloperOwnedById = dto.DeveloperOwnedById,
                UserStoryID = dto.UserStoryID

            };
            return task;
        }

        public static TaskDTO Translate_Task_Entity_To_DTO(Task entity)
        {
            TaskDTO dto = new TaskDTO
            {
                TaskId = entity.TaskId,
                TaskName = entity.TaskName,
                TaskDescription = entity.TaskDescription,
                HoursRemaining = entity.HoursRemaining,
                DeveloperOwnedById = entity.DeveloperOwnedById,
                UserStoryID = entity.UserStoryID

            };
            return dto;
        }

        #endregion
        
    }
}


 