using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.EntityModels.Models
{
    public class ProductBacklog
    {
        public int ProductBacklogId { get; set; }
        public String BacklogTitle { get; set; }
        public Int32 ProjectId { get; set; }

        public virtual Project Project { get; set; }

        // Product backlog properties
        public Int32 ProductOwnerID { get; set; }
        public virtual User ProductOwner { get; set; } // The person who can edit this backlog

        public virtual ICollection<UserStory> Stories { get; set; }

    }
}
