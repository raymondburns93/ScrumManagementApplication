using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ScrumManagementApp.Business;
using Moq;
using ScrumManagementApp.DAL.Repository;
using ScrumManagementApp.EntityModels.Models;
using System.Linq.Expressions;

namespace ScrumManagmentApp.Testing.BusinessLayer
{
    [TestFixture]
    public class ProjectLogicTests
    {
        private Project newProject, existingProject, differentProject;
        private User existingUser;
        private ProjectLogic projectLogic;
        private Mock<IProjectRepository> mockProjectRepo;
        [SetUp]
        public void SetUp()
        {
            projectLogic = new ProjectLogic();
            mockProjectRepo = new Mock<IProjectRepository>(); ;
            newProject = new Project() { ProjectId = 999, ProjectName = "999", ProjectDescription = "New proj", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10) };
            existingProject = new Project() { ProjectId = 1, ProjectName = "1", ProjectDescription = "Existing proj", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10) };
            differentProject = new Project() { ProjectId = 2, ProjectName = "2", ProjectDescription = "Different proj", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10) };

            existingUser = new User() { UserId = 1, Email = "ExistingUser@test.com", Password = "123456" };

        }


        [Test]
        public void GetAllProjects_ExistingProjects_ReturnsExistingProjects()
        {
            mockProjectRepo = new Mock<IProjectRepository>();
            mockProjectRepo.Setup(t => t.GetAll(null)).Returns(existingProject as IList<Project>);
            projectLogic = new ProjectLogic(mockProjectRepo.Object);
            
            IList<Project> result = projectLogic.GetAllProjects();

            Assert.AreEqual(existingProject as IList<Project>, result);
        }

        [Test]
        public void GetProject_ProjectExists_ReturnsProject()
        {
            mockProjectRepo = new Mock<IProjectRepository>();
            mockProjectRepo.Setup(t => t.GetSingle(It.IsAny<Func<Project, bool>>(), null)).Returns(existingProject);
            projectLogic = new ProjectLogic(mockProjectRepo.Object);

            Project result = projectLogic.GetProject(existingProject.ProjectId);
            Assert.AreEqual(existingProject.ProjectId, result.ProjectId);
        }

        [Test]
        public void AddProject_IsValid_AddsNewProject()
        {
            mockProjectRepo = new Mock<IProjectRepository>();
            mockProjectRepo.Setup(t => t.Add(It.IsAny<Project>()));
            mockProjectRepo.Setup(t => t.GetSingle(It.IsAny<Func<Project, bool>>(), null)).Returns((Project)null);

            projectLogic = new ProjectLogic(mockProjectRepo.Object);
            projectLogic.AddProject(newProject, existingUser.UserId);

            mockProjectRepo.Verify(t => t.Add(It.IsAny<Project>()));

        }

        [Test]
        public void GetProjectByProjectname_IsValid_ReturnsProject()
        {
            mockProjectRepo.Setup(t => t.GetSingle(It.IsAny<Func<Project, bool>>(), null)).Returns(existingProject);

            IProjectRepository ProjectRepository = mockProjectRepo.Object;


            projectLogic = new ProjectLogic(ProjectRepository);
            Project result = projectLogic.GetProjectByName(existingProject.ProjectName);

            Assert.AreEqual(existingProject, result);
        }

        public void GetProject_IsValid_ReturnsProject()
        {

            mockProjectRepo.Setup(t => t.GetSingle(It.IsAny<Func<Project, bool>>(), null)).Returns(existingProject);

            IProjectRepository ProjectRepository = mockProjectRepo.Object;

            projectLogic = new ProjectLogic(ProjectRepository);
            Project result = projectLogic.GetProject(existingProject.ProjectId);

            Assert.AreEqual(existingProject, result);

        }

        [Test]
        public void UpdateProject_IsValid_UpdatesProject()
        {

            Mock<IProjectRepository> mockProjectRepo = new Mock<IProjectRepository>();
            mockProjectRepo.Setup(t => t.Update(It.IsAny<Project>()));

            IProjectRepository ProjectRepository = mockProjectRepo.Object;

            ProjectLogic ProjectLogic = new ProjectLogic(ProjectRepository);
            ProjectLogic.UpdateProject(existingProject);

            mockProjectRepo.Verify(t => t.Update(It.IsAny<Project>()));

        }

        [Test]
        public void RemoveProject_IsValid_RemovesProject()
        {

            mockProjectRepo.Setup(t => t.Remove(It.IsAny<Project>()));

            IProjectRepository ProjectRepository = mockProjectRepo.Object;

            projectLogic = new ProjectLogic(ProjectRepository);
            projectLogic.RemoveProject(existingProject);

            mockProjectRepo.Verify(t => t.Remove(It.IsAny<Project>()));

        }





    }
}
