using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.DAL.Repository
{
    public interface IProjectRepository : IGenericRepository<Project>
    {

        void Add(Project project, int userId);
        void AssignRole(Project project, RoleType roles, params int[] userIds);
        bool GetProjectRole(Project projectDTO, int userId, RoleType roleType);

        IList<Project> GetProjectListForUser(int userId);
    }
}
