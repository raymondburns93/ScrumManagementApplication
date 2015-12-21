using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumManagementApp.EntityModels.Models
{
    public class UserSprintRole
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int SprintId { get; set; }
        [Key, Column(Order = 2)]
        public int RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Sprint Sprint { get; set; }
        public virtual Role Role { get; set; }
    }
}
