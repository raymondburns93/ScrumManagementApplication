using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManagementApp.EntityModels.Models
{
    public partial class UserProject
    {
        
    [Key, Column(Order = 0)]
    public int UserId { get; set; }
    [Key, Column(Order = 1)]
    public int ProjectId { get; set; }

    [Key, Column(Order = 2)]
    public int RoleId { get; set; }

    public virtual User user { get; set; }
    public virtual Project project { get; set; }

 
    public virtual Role role { get; set; }

    }
}
