using System;
using System.Runtime.Serialization;

namespace ScrumManagementApp.Common.DTOs
{
    [DataContract]
    public class ProjectDTO
    {
        [DataMember]
        public Int32 ProjectId {get; set;}

        [DataMember]
        public String ProjectName {get; set;}

        [DataMember]
        public String ProjectDescription {get; set;}

        [DataMember]
        public DateTime StartDate {get; set;}

        [DataMember]
        public DateTime? EndDate { get; set; }
    }
}
