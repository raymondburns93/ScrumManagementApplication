using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.DAL
{
    public class ContextInitialiser : CreateDatabaseIfNotExists<ScrumManagementAppContext>
    {
        protected override void Seed(ScrumManagementAppContext context)
        {
            context.roles.Add(new EntityModels.Models.Role { RoleType = EntityModels.Models.RoleType.ProjectManager } );
            context.roles.Add(new EntityModels.Models.Role { RoleType = EntityModels.Models.RoleType.ProductOwner   } );
            context.roles.Add(new EntityModels.Models.Role { RoleType = EntityModels.Models.RoleType.ScrumMaster    } );
            context.roles.Add(new EntityModels.Models.Role { RoleType = EntityModels.Models.RoleType.Developer      } );

            //EF will call SaveChanges itself
        }
    }
}
