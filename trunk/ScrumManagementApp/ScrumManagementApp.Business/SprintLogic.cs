using ScrumManagementApp.DAL.Repository;
using ScrumManagementApp.EntityModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ScrumManagementApp.Business
{

    /// <summary>
    /// Author: James Rainey
    /// Description: Business logic for Sprints.
    /// </summary>
    public class SprintLogic
    {
        private readonly ISprintRepository sprintRepository;
        private readonly ISprintRoleRepository userSprintRoleRepository;
        private readonly ISprintBacklogRepository sprintBacklogRepository;

        /// <summary>
        /// Constructor. Instantiates sprint, sprint role and sprint backlog repisitories.
        /// </summary>
        public SprintLogic()
        {
            sprintRepository = new SprintRepository();
            userSprintRoleRepository = new SprintRoleRepository();
            sprintBacklogRepository = new SprintBacklogRepository();
        }

        /// <summary>
        /// Constructor. Used for unit testing.
        /// </summary>
        /// <param name="sprintRepository"></param>
        public SprintLogic(ISprintRepository sprintRepository)
        {
            this.sprintRepository = sprintRepository;

        }

        /// <summary>
        /// Returns a sprint by the sprint ID
        /// </summary>
        /// <param name="sprintID"></param>
        /// <returns></returns>
        public Sprint getSprint(Int32 sprintID)
        {
            return sprintRepository.GetSingle(s => s.SprintID.Equals(sprintID));
        }      

        /// <summary>
        /// Returns a sprint by the name of the sprint
        /// </summary>
        /// <param name="sprintName"></param>
        /// <returns></returns>
        public Sprint getSprintbyName(String sprintName)
        {
            return sprintRepository.GetSingle(s => s.SprintName.Equals(sprintName));

        }
        /// <summary>
        /// Returns a list of sprints for the given project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public IList<Sprint> GetSprintsForProject(int projectId)
        {
            return sprintRepository.GetList(s => s.ProjectId == projectId, null);
        }

        /// <summary>
        /// Add a new sprint to the sprint repository and set the scrumMaster for the sprint 
        /// </summary>
        /// <param name="sprint"></param>
        /// <param name="scrumMasterId"></param>
        public void AddSprint(Sprint sprint, int scrumMasterId)
        {
            GenericRepository<Role> gr = new GenericRepository<Role>();
            int roleId = gr.GetSingle(rt => rt.RoleType == RoleType.ScrumMaster).RoleId;
            UserSprintRole usr = new UserSprintRole() { UserId = scrumMasterId, SprintId = sprint.SprintID, RoleId = roleId };
            ICollection<UserSprintRole> usersprintrole = new Collection<UserSprintRole>();

            usersprintrole.Add(usr);
            sprint.usr = usersprintrole;
            sprintRepository.Add(sprint);

            SprintBacklog sb = new SprintBacklog() { SprintID = sprint.SprintID, BacklogTitle = sprint.SprintName + "backlog" };
            sprintBacklogRepository.Add(sb);
            sprint.SprintBacklogId = sb.SprintBacklogId;
            sprintRepository.Update(sprint);
         

        }

        /// <summary>
        /// Updates an existing sprint.
        /// </summary>
        /// <param name="sprint"></param>
        public void updateSprint(Sprint sprint)
        {
            sprintRepository.Update(sprint);
        }

        /// <summary>
        /// Removes an existing sprint.
        /// </summary>
        /// <param name="sprint"></param>
        public void removeSprint(Sprint sprint)
        {
            sprintRepository.Remove(sprint);
        }

        /// <summary>
        /// Assign developers to a sprint
        /// </summary>
        /// <param name="sprint"></param>
        /// <param name="userId"></param>
        public void AssignDeveloper(Sprint sprint, int userId)
        {
            sprintRepository.AssignRole(sprint, RoleType.Developer, userId);
        }

        /// <summary>
        /// Return a user entity of the scrum master for the given sprint.
        /// </summary>
        /// <param name="sprintId"></param>
        /// <returns></returns>
        public User GetScrumManagerForSprint(int sprintId)
        {
            UserSprintRole userSprintRole = userSprintRoleRepository.GetSingle(s => s.SprintId == sprintId && s.Role.RoleType == RoleType.ScrumMaster, u => u.User, r => r.Role); 
            if (userSprintRole != null)
            {
                return userSprintRole.User;
            }
            return null;
        }

        /// <summary>
        /// Returns a list of user entities, each of which is an assigned-developer of the sprint.
        /// </summary>
        /// <param name="sprintId"></param>
        /// <returns></returns>
        public IList<User> GetDevelopersForSprint(int sprintId)
        {  
            IList<UserSprintRole> userSprintRole = userSprintRoleRepository.GetList(s => s.SprintId == sprintId && s.Role.RoleType == RoleType.Developer, u => u.User, r => r.Role);
            if(userSprintRole != null)
            {
                return userSprintRole.Select(u => u.User).ToList();   
            }
            return null;
        }

        /// <summary>
        /// Returns a list of sprint entities, each of which the user is associated with (developer/scrum master)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Sprint> GetSprintsForUser(int userId)
        {
            return userSprintRoleRepository.GetList(usr => usr.UserId == userId,usr => usr.Sprint).Select(usr => usr.Sprint).ToList();
        }
    }
}
