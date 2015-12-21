using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScrumManagementApp.EntityModels.Models
{
    public partial class User
    {

        public int UserId { get; set; }

        [StringLength(50)]
        public String Email { get; set; }

        [StringLength(50)]
        public String Password { get; set; }

        [StringLength(100)]
        public String Skillset { get; set; }

        [StringLength(20)]
        public String Alias { get; set; }

        [StringLength(20)]
        public String Firstname { get; set; }

        [StringLength(20)]
        public String Lastname { get; set; }

        public bool IsProductOwner { get; set; }

        public bool IsScrumMaster { get; set; }

        public bool IsDeveloper { get; set; }

        public virtual ICollection<UserProject> upr { get; set; }
    }
}
