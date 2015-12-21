using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.Common.DTOs
{
    [DataContract]
    public class ProductBacklogDTO
    {
        [DataMember]
        public int ProductBacklogId { get; set; }

        [DataMember]
        public String BacklogTitle { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public DateTime UpdatedDate { get; set; }

        [DataMember]
        public bool Locked { get; set; }

        [DataMember]
        public Int32 ProjectID { get; set; }

        [DataMember]
        public Int32 ProductOwnerID { get; set; }

        [DataMember]
        public Int32 SprintID { get; set; }
         
    }
}
