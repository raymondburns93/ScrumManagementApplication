using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.EntityModels.Models
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public RoleType RoleType { get; set; }
    }

    public enum RoleType
    {
        ProjectManager,
        ProductOwner,
        ScrumMaster,
        Developer
    };

}
