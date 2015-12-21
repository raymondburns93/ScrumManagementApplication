using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ScrumManagementApp.Common.DTOs
{
    [DataContract]
    public class SprintBacklogDTO
    {

        [DataMember]
        public Int32 SprintBacklogID { get; set; }
        [DataMember]
        public String Title { get; set; }
        [DataMember]
        public Int32 SprintID { get; set; }


    }
}
