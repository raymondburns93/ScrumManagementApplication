using System;
using System.Runtime.Serialization;

namespace ScrumManagementApp.Common.DTOs
{
    [DataContract]
    public class UserStoryDTO
    {
        [DataMember]
        public Int32 UserStoryID { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public bool Locked { get; set; }

        [DataMember]
        public int? Priority { get; set; }

        [DataMember]
        public Int32 ProductBacklogId { get; set; }
    }
}
