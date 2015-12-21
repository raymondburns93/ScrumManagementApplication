using System;
using System.Runtime.Serialization;

namespace ScrumManagementApp.Common.DTOs
{
    // Used for authentication
    [DataContract]
    public class CondensedUserDTO
    {
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public String Password { get; set; }
    }
}
