using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.DAL.Repository
{
    public interface ISprintRepository : IGenericRepository<Sprint>
    {
        void AssignRole(Sprint sprint, RoleType roleType, params int[] userIds);

        bool GetIfUserIsAssignedToSprint(Sprint sprint, int userId, RoleType roleType);

        IList<Sprint> GetSprintsForUser(int userId);
    }
}
    