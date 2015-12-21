using System.Collections.Generic;
using ScrumManagementApp.Business;
using ScrumManagementApp.Common.DTOs;
using ScrumManagementApp.EntityModels.Models;
using ScrumManagementApp.WebService.Common;
using ScrumManagementApp.WebService.Interfaces;

namespace ScrumManagementApp.WebService.Services
{
    public class SprintService : ISprintService
    {

        SprintLogic sprintLogic = new SprintLogic();

        public SprintDTO CreateSprint(SprintDTO sprintDTO, int scrumMasterId)
        {
            sprintLogic.AddSprint(EntityTranslations.Translate_Sprint_DTO_To_Entity(sprintDTO),scrumMasterId);

            return sprintDTO;

        }

        public IList<SprintDTO> GetSprintsForProject(int projectId)
        {
            IList<Sprint> sprints = sprintLogic.GetSprintsForProject(projectId);
            List<SprintDTO> sprintsdtos = new List<SprintDTO>();

            foreach (Sprint s in sprints)
            {
                    sprintsdtos.Add(EntityTranslations.Translate_Sprint_Entity_To_DTO(s));
            }
      
            return sprintsdtos;
        }

        public void UpdateSprint(SprintDTO sprintDTO)
        {
            if (sprintDTO != null)
            {
                Sprint toSave = EntityTranslations.Translate_Sprint_DTO_To_Entity(sprintDTO);
                sprintLogic.updateSprint(toSave);
            }
        }

        public void RemoveSprint(SprintDTO sprintDTO)
        {
            Sprint toRemove = EntityTranslations.Translate_Sprint_DTO_To_Entity(sprintDTO);
            sprintLogic.removeSprint(toRemove);
        }

        public void AssignDeveloperToSprint(SprintDTO sprintDTO, int userId)
        {
            if (sprintDTO != null)
            {
                Sprint sprint = EntityTranslations.Translate_Sprint_DTO_To_Entity(sprintDTO);
                sprintLogic.AssignDeveloper(sprint, userId);
            }
        }

        public UserDTO GetScrumManagerForSprint(int sprintId)
        {
            User scrumMaster = sprintLogic.GetScrumManagerForSprint(sprintId);
            if (scrumMaster != null)
            {
                return EntityTranslations.Translate_User_Entity_To_DTO(scrumMaster);
            }
            return null;
        }

        public List<UserDTO> GetDevelopersForSprint(int sprintId)
        {
            IList<User> developers = sprintLogic.GetDevelopersForSprint(sprintId);

            List<UserDTO> developersDTO = new List<UserDTO>();

            foreach (User developer in developers)
            {
                    developersDTO.Add(EntityTranslations.Translate_User_Entity_To_DTO(developer));    
            }
            return developersDTO;
        }

        public List<SprintDTO> GetSprintsForUser(int userId)
        {
            IList<Sprint> sprints = sprintLogic.GetSprintsForUser(userId);

            List<SprintDTO> sprintDTOs = new List<SprintDTO>();
            foreach (Sprint s in sprints)
            {
                sprintDTOs.Add(EntityTranslations.Translate_Sprint_Entity_To_DTO(s));
            }
            return sprintDTOs;
        }

    }
}
