using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;
using ScrumManagementApp.Common.DTOs;
using System;


namespace ScrumManagmentApp.Testing.Client
{
    [TestFixture]
    public class ProductBacklogViewModelTest
    {
        ProductBacklogViewModel vm;
        ProjectDTO project;
        UserDTO user;
        bool isValid;

        [SetUp]
        public void Init()
        {
            project = new ProjectDTO();
            user = new UserDTO();
            vm = new ProductBacklogViewModel(project,user);
            isValid = false;
        }

        #region empty fields tests
        [Test]
        public void ValidateNewUserStoryEmptyText()
        {
            vm.UserStoryText = "";
            vm.UserStoryPriority = 1;
            vm.ValidateUserStory(ref isValid);
            Assert.IsFalse(isValid);
        }

        [Test]
        public void ValidateNewUserStory0UserStoryPoints()
        {
            vm.UserStoryText = "test";
            vm.UserStoryPriority = 0;
            vm.ValidateUserStory(ref isValid);
            Assert.IsFalse(isValid);
        }


        #endregion empty fields tests

        [Test]
        public void ValidateNewUserStory_Success()
        {
            vm.UserStoryText = "test";
            vm.UserStoryPriority = 1;
            vm.ValidateUserStory(ref isValid);
            Assert.IsTrue(isValid);
        }


    }
}
