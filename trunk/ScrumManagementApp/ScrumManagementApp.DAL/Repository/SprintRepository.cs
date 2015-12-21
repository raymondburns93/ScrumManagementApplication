using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.DAL.Repository
{
    public class SprintRepository : GenericRepository<Sprint>, ISprintRepository
    {
        public void AssignRole(Sprint sprint, RoleType roleType, params int[] userIds)
        {
            using (var context = new ScrumManagementAppContext())
            {

                Role r = context.roles.Where(m => m.RoleType == roleType).SingleOrDefault();


                foreach (int i in userIds)
                {
                    var sr = new UserSprintRole();
                    sr.SprintId = sprint.SprintID;
                    sr.UserId =  i;
                    sr.RoleId = r.RoleId;

                    context.Entry<UserSprintRole>(sr).State = System.Data.Entity.EntityState.Added;
                }

                context.SaveChanges();
            }
        }

        public bool GetIfUserIsAssignedToSprint(Sprint sprint, int userId, RoleType roleType)
        {
            using (var context = new ScrumManagementAppContext())
            {

                return context.SprintRoles.Where(sr => sr.SprintId.Equals(sprint.SprintID) && sr.UserId.Equals(userId)).Select(sr => sr.Role.RoleType == roleType).FirstOrDefault();
            }
        }

        public IList<Sprint> GetSprintsForUser(int userId)
        {
            using (var context = new ScrumManagementAppContext())
            {
                return context.SprintRoles.Where(sr => sr.UserId == userId).Select(sr => sr.Sprint).ToList();
            }
        }
    }
}
