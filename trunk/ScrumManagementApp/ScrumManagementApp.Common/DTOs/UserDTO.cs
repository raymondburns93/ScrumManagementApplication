using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ScrumManagementApp.Common.DTOs
{
    // Used for read-writes
    [DataContract]
    public class UserDTO
    {
        [DataMember]
        public Int32 UserId { get; set; }

        [DataMember]
        [StringLength(20, ErrorMessage = "No longer than 20 characters")]
        public String Firstname { get; set; }

        [DataMember]
        [StringLength(20, ErrorMessage = "No longer than 20 characters")]
        public String Lastname { get; set; }

        [DataMember]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50, ErrorMessage = "No longer than 50 characters")]
        public String Email { get; set; }

        [DataMember]
        [StringLength(50, ErrorMessage = "No longer than 50 characters")]
        public String Password { get; set; }

        [DataMember]
        public bool IsProductOwner { get; set; }

        [DataMember]
        public bool IsScrumMaster { get; set; }

        [DataMember]
        public bool IsDeveloper { get; set; }

        [DataMember]
        public String SkillSet { get; set; }
        
        [DataMember]
        public String Alias { get; set; }
    }

}
