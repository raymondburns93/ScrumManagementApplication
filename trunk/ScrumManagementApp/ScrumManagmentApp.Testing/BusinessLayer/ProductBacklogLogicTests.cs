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
    public class ProductBacklogLogicTests
    {
        private ProductBacklog newproductBacklog, existingproductBacklog, differentproductBacklog;
        private UserStory newUserStory, existingUserStory, differentUserStory;

        private User existingUser;
        private ProductBacklogLogic productBacklogLogic;
        private Mock<IProductBacklogRepository> mockProductBacklogRepo;
        private Mock<IUserStoryRepository> mockUserStoryRepo;

        //private Mock<IAssignedUserStoryRepository> assignedUserStorygRepo; needs to be developed

        [SetUp]
        public void SetUp()
        {
            productBacklogLogic = new ProductBacklogLogic();
            mockProductBacklogRepo = new Mock<IProductBacklogRepository>();
            mockUserStoryRepo = new Mock<IUserStoryRepository>();

            newproductBacklog = new ProductBacklog() { ProductBacklogId = 999, BacklogTitle = "new", ProductOwnerID=1, ProjectId=1  };
            existingproductBacklog = new ProductBacklog() { ProductBacklogId = 1, BacklogTitle = "exisiting", ProductOwnerID = 1, ProjectId = 1};
            differentproductBacklog = new ProductBacklog() { ProductBacklogId = 2, BacklogTitle = "different", ProductOwnerID = 1, ProjectId = 1 };

            newUserStory = new UserStory() { UserStoryID = 999, Description = "new", Locked=false, ProductBacklogId =2, Priority = 1 };
            existingUserStory = new UserStory() { UserStoryID = 1, Description = "exisiting", Locked = false, ProductBacklogId = 1, Priority = 2 };
            differentUserStory = new UserStory() { UserStoryID = 2, Description = "different", Locked = false, ProductBacklogId = 1, Priority = 5 };

        }

        [Test]
        public void GetProductBacklogTest()
        {
            mockProductBacklogRepo = new Mock<IProductBacklogRepository>();
            mockProductBacklogRepo.Setup(t => t.GetSingle(It.IsAny<Func<ProductBacklog, bool>>(), null)).Returns(existingproductBacklog);

            productBacklogLogic = new ProductBacklogLogic(mockProductBacklogRepo.Object);
            ProductBacklog result = productBacklogLogic.GetProductBacklog(existingproductBacklog.ProjectId);
            Assert.AreEqual(existingproductBacklog.ProductBacklogId, result.ProductBacklogId);
        }

        [Test]
        public void GetProductBacklogUserStoriesTest()
        {
            mockProductBacklogRepo = new Mock<IProductBacklogRepository>();
            mockUserStoryRepo.Setup(t => t.GetSingle(It.IsAny<Func<UserStory, bool>>(), null)).Returns(existingUserStory);
            mockUserStoryRepo.Setup(t => t.GetSingle(It.IsAny<Func<UserStory, bool>>(), null)).Returns(differentUserStory);
            productBacklogLogic = new ProductBacklogLogic(mockProductBacklogRepo.Object);

            var userStoriesReturned = productBacklogLogic.GetProductBacklogUserStories(existingproductBacklog.ProjectId);
            bool testResult = userStoriesReturned.Contains(existingUserStory) && userStoriesReturned.Contains(differentUserStory);
            Assert.IsTrue(testResult);
        }

        [Test]
        public void AddUserStoryTest()
        {
            mockProductBacklogRepo = new Mock<IProductBacklogRepository>();
            mockUserStoryRepo.Setup(t => t.Add(It.IsAny<UserStory>()));
            mockUserStoryRepo.Setup(t => t.GetSingle(It.IsAny<Func<UserStory, bool>>(), null)).Returns((UserStory)null);

            IUserStoryRepository storyRepository = mockUserStoryRepo.Object;

            productBacklogLogic = new ProductBacklogLogic(storyRepository);
            productBacklogLogic.AddUserStory(differentproductBacklog.ProjectId, newUserStory);

            mockUserStoryRepo.Verify(t => t.Add(It.IsAny<UserStory>()));
        }


        [Test]
        public void DeleteUserStoryTest()
        {
            mockUserStoryRepo.Setup(t => t.Remove(It.IsAny<UserStory>()));

            IUserStoryRepository storyRepository = mockUserStoryRepo.Object;

            productBacklogLogic = new ProductBacklogLogic(storyRepository);
            productBacklogLogic.DeleteUserStory(existingproductBacklog.ProjectId, existingUserStory.UserStoryID);

            mockUserStoryRepo.Verify(t => t.Remove(It.IsAny<UserStory>()));
        }


        [Test]
        public void EditUserStoryTest()
        {
            mockUserStoryRepo = new Mock<IUserStoryRepository>();
            mockUserStoryRepo.Setup(t => t.Update(It.IsAny<UserStory>()));

            IUserStoryRepository storyRepository = mockUserStoryRepo.Object;

            productBacklogLogic = new ProductBacklogLogic(storyRepository);
            productBacklogLogic.EditUserStory(existingproductBacklog.ProjectId, existingUserStory);

            mockUserStoryRepo.Verify(t => t.Update(It.IsAny<UserStory>()));
        }

        [Test]
        public void ReprioritiseUserStoryStoryTest()
        {
            int oldUserStoryPriority = (int)existingUserStory.Priority;
            int newUserStoryPriority = 3;
            existingUserStory.Priority = newUserStoryPriority;


            mockUserStoryRepo = new Mock<IUserStoryRepository>();
            mockUserStoryRepo.Setup(t => t.Update(It.IsAny<UserStory>()));

            IUserStoryRepository storyRepository = mockUserStoryRepo.Object;

            productBacklogLogic = new ProductBacklogLogic(storyRepository);
            productBacklogLogic.EditUserStory(existingproductBacklog.ProjectId, existingUserStory);

            //test it updates ok
            mockUserStoryRepo.Verify(t => t.Update(It.IsAny<UserStory>()));

            //test priroity has changed
            Assert.AreEqual(newUserStoryPriority, existingUserStory.Priority);
        }
    }
}
