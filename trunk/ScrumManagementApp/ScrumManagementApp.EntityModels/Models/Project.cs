using System;
using ScrumManagementApp.EntityModels.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ScrumManagementApp.EntityModels.Models
{
    public partial class Project
    {

        public Int32 ProjectId { get; set; }

        public String ProjectName { get; set; }

        public String ProjectDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? ProductBacklogId { get; set; }
        public ProductBacklog ProductBacklog { get; set; }

        public ICollection<UserProject> upr  {get; set; }

        public virtual ICollection<Sprint> sprints { get; set; }

    }
}
