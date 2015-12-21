using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.EntityModels.Models
{
    public class Task
    {
        public Int32 TaskId { get; set; }
        public String TaskName { get; set; }
        public String TaskDescription { get; set; }

        public TimeSpan HoursRemaining { get; set; }

        public int? DeveloperOwnedById { get; set; }

        public User DeveloperOwnedBy { get; set; }

        public Int32 UserStoryID { get; set; }

        public UserStory UserStory { get; set; }
    }

}
