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
    public class SprintLogicTests
    {
        private Sprint newSprint, existingSprint, differentSprint, sprintWithNoUsers;
        private User existingUser;
        private SprintLogic sprintLogic;
        private Mock<ISprintRepository> mockSprintRepo;
        List<User> scrumMasters, developers;

        [SetUp]
        public void SetUp()
        {
            sprintLogic = new SprintLogic();
            mockSprintRepo = new Mock<ISprintRepository>();
            developers = new List<User>();
            developers.Add(new User() { UserId = 3, Email = "developer@test.com", Password = "12345" });
            scrumMasters = new List<User>();
            scrumMasters.Add(new User() { UserId = 4, Email = "scrumMaster@test.com", Password = "12345" });

            newSprint = new Sprint() { SprintID = 999,StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10), ProjectId = 1, SprintName="new", SprintBacklogId = 4};
            existingSprint = new Sprint() { SprintID = 1, StartDate = DateTime.Now.AddDays(-50), EndDate = DateTime.Now.AddDays(-10), ProjectId = 1, SprintName = "existing", SprintBacklogId = 1};
            differentSprint = new Sprint() { SprintID = 2, StartDate = DateTime.Now.AddDays(-300), EndDate = DateTime.Now.AddDays(-200), ProjectId = 2, SprintName = "different", SprintBacklogId = 2};
        }

        [Test]
        public void getSprintTest()
        {
            mockSprintRepo = new Mock<ISprintRepository>();
            mockSprintRepo.Setup(t => t.GetSingle(It.IsAny<Func<Sprint, bool>>(), null)).Returns(existingSprint);

            sprintLogic = new SprintLogic(mockSprintRepo.Object);
            Sprint result = sprintLogic.getSprint(existingSprint.SprintID);
            Assert.AreEqual(existingSprint.SprintID, result.SprintID);
        }

        [Test]
        public void GetSprintsForProjectTest()
        {
            mockSprintRepo = new Mock<ISprintRepository>();
            mockSprintRepo.Setup(t => t.GetSingle(It.IsAny<Func<Sprint, bool>>(), null)).Returns(existingSprint);
            mockSprintRepo.Setup(t => t.GetSingle(It.IsAny<Func<Sprint, bool>>(), null)).Returns(newSprint);
            sprintLogic = new SprintLogic(mockSprintRepo.Object);

            var returnedSprints = sprintLogic.GetSprintsForProject(existingSprint.ProjectId);
            bool testResult = returnedSprints.Contains(existingSprint) && returnedSprints.Contains(newSprint);

            Assert.IsTrue(testResult);
        }

        [Test]
        public void addSprintTest()
        {
            mockSprintRepo = new Mock<ISprintRepository>();
            mockSprintRepo.Setup(t => t.Add(It.IsAny<Sprint>()));
            mockSprintRepo.Setup(t => t.GetSingle(It.IsAny<Func<Sprint, bool>>(), null)).Returns((Sprint)null);

            ISprintRepository sprintRepository = mockSprintRepo.Object;
            sprintLogic = new SprintLogic(sprintRepository);
            sprintLogic.AddSprint(newSprint, scrumMasters.First().UserId);

            mockSprintRepo.Verify(t => t.Add(It.IsAny<Sprint>()));
        }

        [Test]
        public void updateSprintTest()
        {
            Mock<ISprintRepository> mockSprintRepo = new Mock<ISprintRepository>();
            mockSprintRepo.Setup(t => t.Update(It.IsAny<Sprint>()));

            ISprintRepository SprintRepository = mockSprintRepo.Object;

            sprintLogic = new SprintLogic(SprintRepository);
            sprintLogic.updateSprint(existingSprint);

            mockSprintRepo.Verify(t => t.Update(It.IsAny<Sprint>()));
        }

        [Test]
        public void removeSprintTest()
        {

            mockSprintRepo.Setup(t => t.Remove(It.IsAny<Sprint>()));

            ISprintRepository SprintRepository = mockSprintRepo.Object;

            sprintLogic = new SprintLogic(SprintRepository);
            sprintLogic.removeSprint(existingSprint);

            mockSprintRepo.Verify(t => t.Remove(It.IsAny<Sprint>()));

        }

        [Test]
        public void AssignDeveloper_NewDeveloper_AssignsDeveloper()
        {
            int[] myarray = developers.Select(u => u.UserId).ToArray();

            Mock<ISprintRepository> mockSprintRepo = new Mock<ISprintRepository>();
            mockSprintRepo.Setup(t => t.AssignRole(existingSprint, RoleType.Developer, myarray)).Verifiable("sprint repository was not called");

            ISprintRepository sprintRepository = mockSprintRepo.Object;

            SprintLogic sprintLogic = new SprintLogic(sprintRepository);
            sprintLogic.AssignDeveloper(existingSprint, developers[0].UserId);

        }

    }
}
