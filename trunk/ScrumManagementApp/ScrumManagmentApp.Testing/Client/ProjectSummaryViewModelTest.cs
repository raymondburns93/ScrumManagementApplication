using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagmentApp.Testing.Client
{
    /// <summary>
    /// Author: Andrew Baird
    /// </summary>

    [TestFixture]
    public class ProjectSummaryViewModelTest
    {
        ProjectSummaryViewModel vm;
        ProjectDTO project;
        UserDTO user;

        [SetUp]
        public void Init()
        {
            project = new ProjectDTO();
            user = new UserDTO();
            vm = new ProjectSummaryViewModel(project, user);
        }

        //[Test]
    }
}
