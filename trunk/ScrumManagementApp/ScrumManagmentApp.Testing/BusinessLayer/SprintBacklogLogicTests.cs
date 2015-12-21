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
    public class SprintBacklogLogicTests
    {
        private SprintBacklogLogic _sprintBacklogLogic;
        private Mock<ISprintBacklogRepository> _mockSprintBacklogRepo;
        private Mock<IUserStoryRepository> _mockUserStoryRepo;
        private SprintBacklog _newSprintBacklog, _existingSprintBacklog, _differentSprintBacklog;
        private UserStory _newUserStory, _existingUserStory, _differentUserStory;

        [SetUp]
        public void SetUp()
        {
            _sprintBacklogLogic = new SprintBacklogLogic();
            _mockSprintBacklogRepo = new Mock<ISprintBacklogRepository>();
            _mockUserStoryRepo = new Mock<IUserStoryRepository>();

            _newSprintBacklog = new SprintBacklog()
            {
                SprintBacklogId = 999,
                BacklogTitle = "New Sprint Backlog",
                SprintID = 1
            };

            _existingSprintBacklog = new SprintBacklog()
            {
                SprintBacklogId = 1,
                BacklogTitle = "Existing Sprint Backlog",
                SprintID = 1
            };

            _differentSprintBacklog = new SprintBacklog()
            {
                SprintBacklogId = 2,
                BacklogTitle = "Different Sprint Backlog",
                SprintID = 1
            };

            _newUserStory = new UserStory() { UserStoryID = 999, Description = "new", Locked = false, SprintBacklogId = 1, Priority = 1 };
            _existingUserStory = new UserStory() { UserStoryID = 1, Description = "exisiting", Locked = false, SprintBacklogId = 1, Priority = 2 };
            _differentUserStory = new UserStory() { UserStoryID = 2, Description = "different", Locked = false, SprintBacklogId = 1, Priority = 5 };

        }

        [Test]
        public void GetSprintBacklogTest()
        {
            _mockSprintBacklogRepo.Setup(t => t.GetSingle(It.IsAny<Func<SprintBacklog, bool>>(), null)).Returns(_existingSprintBacklog);

            SprintBacklog result = _sprintBacklogLogic.GetSprintBacklog(_existingSprintBacklog.SprintBacklogId);
            Assert.AreEqual(_existingSprintBacklog.SprintBacklogId, result.SprintBacklogId);
        }

        [Test]
        public void GetSprintBacklogUserStoriesTest()
        {
            _mockUserStoryRepo.Setup(t => t.GetSingle(It.IsAny<Func<UserStory, bool>>(), null)).Returns(_existingUserStory);
            UserStory result = _sprintBacklogLogic.GetSprintBacklogUserStories(_existingUserStory.UserStoryID).FirstOrDefault();
            Assert.AreEqual(_existingUserStory.UserStoryID, result.UserStoryID);
        }

        [Test]
        public void AddUserStoryTest()
        {
            _mockUserStoryRepo.Setup(t => t.Add(It.IsAny<UserStory>()));
            _mockUserStoryRepo.Setup(t => t.GetSingle(It.IsAny<Func<UserStory, bool>>(), null)).Returns((UserStory)null);

            IUserStoryRepository storyRepository = _mockUserStoryRepo.Object;

            SprintBacklogLogic logic = new SprintBacklogLogic(storyRepository);
            logic.AddUserStory(_differentSprintBacklog.SprintBacklogId, _differentUserStory);

            _mockUserStoryRepo.Verify(t => t.Add(It.IsAny<UserStory>()));
        }

        [Test]
        public void DeleteUserStoryTest()
        {
            _mockUserStoryRepo.Setup(t => t.Remove(It.IsAny<UserStory>()));

            IUserStoryRepository storyRepository = _mockUserStoryRepo.Object;

            _sprintBacklogLogic = new SprintBacklogLogic(storyRepository);
            _sprintBacklogLogic.DeleteUserStory(_existingSprintBacklog.SprintBacklogId, _existingUserStory.UserStoryID);

            _mockUserStoryRepo.Verify(t => t.Remove(It.IsAny<UserStory>()));
        }

        [Test]
        public void EditUserStoryTest()
        {
            _mockUserStoryRepo = new Mock<IUserStoryRepository>();
            _mockUserStoryRepo.Setup(t => t.Update(It.IsAny<UserStory>()));

            IUserStoryRepository storyRepository = _mockUserStoryRepo.Object;

            _sprintBacklogLogic = new SprintBacklogLogic(storyRepository);
            _sprintBacklogLogic.EditUserStory(_existingUserStory);

            _mockUserStoryRepo.Verify(t => t.Update(It.IsAny<UserStory>()));
        }

        [Test]
        public void ReprioritiseUserStoryStoryTest()
        {
            Int32 oldUserStoryPriority = (Int32)_existingUserStory.Priority;
            Int32 newUserStoryPriority = 3;
            _existingUserStory.Priority = newUserStoryPriority;


            _mockUserStoryRepo = new Mock<IUserStoryRepository>();
            _mockUserStoryRepo.Setup(t => t.Update(It.IsAny<UserStory>()));

            IUserStoryRepository storyRepository = _mockUserStoryRepo.Object;

            _sprintBacklogLogic = new SprintBacklogLogic(storyRepository);
            _sprintBacklogLogic.EditUserStory(_existingUserStory);

            //test it updates ok
            _mockUserStoryRepo.Verify(t => t.Update(It.IsAny<UserStory>()));

            //test priroity has changed
            Assert.AreEqual(newUserStoryPriority, _existingUserStory.Priority);
        }

    }
}
