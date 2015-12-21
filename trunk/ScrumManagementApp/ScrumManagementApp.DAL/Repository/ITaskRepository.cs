using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.DAL.Repository
{
    public interface ITaskRepository : IGenericRepository<EntityModels.Models.Task>
    {
    }
}
