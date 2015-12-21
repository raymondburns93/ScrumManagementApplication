using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.EntityModels.Models
{
    public class SprintBacklog
    {
        public int SprintBacklogId { get; set; }

        public String BacklogTitle { get; set; }

        public Int32 SprintID { get; set; }


        public virtual Sprint sprint { get; set; }


        public virtual ICollection<UserStory> Stories { get; set; }
        

    }
}
