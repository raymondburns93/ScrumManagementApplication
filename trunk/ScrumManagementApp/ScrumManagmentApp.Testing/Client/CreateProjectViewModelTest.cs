using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;
using ScrumManagementApp.Common.DTOs;
using System;


namespace ScrumManagmentApp.Testing.Client
{
    [TestFixture]
    public class CreateProjectViewModelTest
    {
        CreateProjectViewModel vm;
        UserDTO user;

        [SetUp]
        public void Init()
        {
            user = new UserDTO();
            vm = new CreateProjectViewModel(user);
        }

        #region empty fields tests
        [Test]
        public void ValidateNewProjectEmptyName()
        {
            vm.ProjectDescription = "test";
            vm.EndDateString = DateTime.Now.ToString();
            vm.StartDateString = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

        [Test]
        public void ValidateNewProjectEmptyDescription()
        {
            vm.ProjectName = "test";
            vm.EndDateString = DateTime.Now.ToString();
            vm.StartDateString = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

        [Test]
        public void ValidateNewProjectEmptyStartDate()
        {
            vm.ProjectName = "test";
            vm.ProjectDescription = "test";
            vm.EndDateString = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

        [Test]
        public void ValidateNewProjectEmptyEndDate()
        {
            vm.ProjectName = "test";
            vm.ProjectDescription = "test";
            vm.StartDateString = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

        #endregion empty fields tests

        [Test]
        public void ValidateNewProject_StartDateAfterEndDate()
        {
            vm.ProjectName = "test";
            vm.ProjectDescription = "test";
            vm.StartDateString = DateTime.Now.AddDays(1).ToString();
            vm.EndDateString = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

    }
}
