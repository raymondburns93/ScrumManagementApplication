using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.EntityModels.Models
{
    public class UserStory
    {
        public Int32 UserStoryID { get; set; }
        public String Description { get; set; }
        public bool Locked { get; set; }

        public int? Priority { get; set; }

        public int ProductBacklogId { get; set; }

        public int? SprintBacklogId { get; set; }
   
        public ProductBacklog productBacklog { get; set; }

        public SprintBacklog SprintBacklog { get; set; }

        public virtual ICollection<Task> tasks { get; set; }

    }
}
