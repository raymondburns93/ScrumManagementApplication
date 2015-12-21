using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.EntityModels.Models
{
    public class Sprint
    {
        public Int32 SprintID { get; set; }
        public String SprintName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
     
        public Int32 ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int? SprintBacklogId { get; set; }
        public virtual SprintBacklog SprintBacklog { get; set; }

        public ICollection<UserSprintRole> usr { get; set; }
    }
}
