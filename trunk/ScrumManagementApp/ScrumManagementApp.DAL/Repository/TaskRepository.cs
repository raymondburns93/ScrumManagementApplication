﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.DAL.Repository
{
    public class TaskRepository : GenericRepository<EntityModels.Models.Task>, ITaskRepository
    {
    }
}