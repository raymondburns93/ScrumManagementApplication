using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.Common.DTOs
{
    [DataContract]
    public class SprintDTO
    {
        [DataMember]
        public Int32 SprintId { get; set; }

        [DataMember]
        public String SprintName { get; set; }

        [DataMember]
        public String ScrumMaster { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public Int32 ProjectId { get; set; }

        [DataMember]
        public Int32? SprintBacklogId { get; set; }

    }
}
