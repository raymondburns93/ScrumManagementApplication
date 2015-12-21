using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.Common.DTOs
{
    [DataContract]
    public class TaskDTO
    {
        [DataMember]
        public Int32 TaskId { get; set; }

        [DataMember]
        public String TaskName { get; set; }
        
        [DataMember]
        public String TaskDescription { get; set; }

        [DataMember]
        public TimeSpan HoursRemaining { get; set; }

        [DataMember]
        public int? DeveloperOwnedById { get; set; }

        [DataMember]
        public Int32 UserStoryID { get; set; }

    
    }
}
