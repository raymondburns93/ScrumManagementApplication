using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumManagementApp.EntityModels.Models;
using System.Collections.ObjectModel;

namespace ScrumManagementApp.DAL.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {

       public void Add(Project project,int userId)
        {
            using (var context = new ScrumManagementAppContext())
            {

                Role r = context.roles.Where(m => m.RoleType == RoleType.ProjectManager).SingleOrDefault();

                var v = new UserProject();
                v.ProjectId = project.ProjectId;
                v.UserId = userId;
                v.RoleId = r.RoleId;

                context.Entry<Project>(project).State = System.Data.Entity.EntityState.Added;
                context.Entry<UserProject>(v).State = System.Data.Entity.EntityState.Added;

                context.SaveChanges();

                ProductBacklog pb = new ProductBacklog();
                pb.ProjectId = project.ProjectId;
                project.ProductBacklog = pb;
                
                context.ProductBacklogs.Add(pb);
                context.SaveChanges();

            }
        }

        
       public void AssignRole(Project project, RoleType roleType, params int[] userIds)
       {
           using (var context = new ScrumManagementAppContext())
           {

               Role r = context.roles.Where(m => m.RoleType == roleType).SingleOrDefault();

               foreach (int i in userIds)
               {
                   var v = new UserProject();
                   v.ProjectId = project.ProjectId;
                   v.UserId = i;
                   v.RoleId = r.RoleId;

                   context.Entry<UserProject>(v).State = System.Data.Entity.EntityState.Added;
               }

               context.SaveChanges();

           }
       }

       public bool GetProjectRole(Project projectDTO, int userId, RoleType roleType)
       {

           using (var context = new ScrumManagementAppContext())
           {

               return context.UserProjects.Where(p => p.ProjectId.Equals(projectDTO.ProjectId) && p.UserId.Equals(userId)).Select(p => p.role.RoleType == roleType).FirstOrDefault();
              // List<bool> v = context.UserProjects.Where(p => p.ProjectId.Equals(projectDTO.ProjectId) && p.UserId.Equals(userId)).Select(m => m.role.Equals(roleType)).ToList();
               
               //foreach (UserProject up in v)
               //{
               //    up.
               //}


               //List<UserProject> up = context.UserProjects.Where(p => p.ProjectId.Equals(projectDTO.ProjectId) && p.UserId.Equals(userId)).ToList();

               //up.

           }

       }

       /// <summary>
       /// Author : Raymond Burns
       /// Description : Retrieves list of projects specified user is associated with
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
       public IList<Project> GetProjectListForUser(int userId)
       {
           using (var context = new ScrumManagementAppContext())
           {
              
               IList<Project> projectRole = context.UserProjects.Where(upr => upr.UserId == userId).Select(upr => upr.project).ToList();

               IList<Project> sprintRole = context.SprintRoles.Where(usr => usr.UserId == userId).Select(usr => usr.Sprint)
                      .Select(p => p.Project).ToList();

               return projectRole.Concat(sprintRole).Distinct().ToList();
              
           }
       }
    }
}
