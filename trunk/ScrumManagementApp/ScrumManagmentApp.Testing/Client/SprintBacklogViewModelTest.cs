using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagmentApp.Testing.Client
{
    [TestFixture]
    public class SprintBacklogViewModelTest
    {
        SprintBacklogViewModel vm;
        SprintDTO sprint;
        ProjectDTO project;
        UserDTO user;

        [SetUp]
        public void Init()
        {
            vm = new SprintBacklogViewModel(project, sprint, user);
            sprint = new SprintDTO();
            project = new ProjectDTO();
            user = new UserDTO();
        }
    }
}
