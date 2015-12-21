using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;
using System;


namespace ScrumManagmentApp.Testing
{
    [TestFixture]
    public class CreateProjectViewModelTest
    {
        CreateProjectViewModel vm;

        [SetUp]
        public void Init()
        {
            vm = new CreateProjectViewModel();
        }

        #region empty fields tests
        [Test]
        public void ValidateNewProjectEmptyName()
        {
            vm.txtProjectDescriptionProperty = "test";
            vm.dpEndDateProperty = DateTime.Now.ToString();
            vm.dpStartDateProperty = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

        [Test]
        public void ValidateNewProjectEmptyDescription()
        {
            vm.txtProjectNameProperty = "test";
            vm.dpEndDateProperty = DateTime.Now.ToString();
            vm.dpStartDateProperty = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

        [Test]
        public void ValidateNewProjectEmptyStartDate()
        {
            vm.txtProjectNameProperty = "test";
            vm.txtProjectDescriptionProperty = "test";
            vm.dpEndDateProperty = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

        [Test]
        public void ValidateNewProjectEmptyEndDate()
        {
            vm.txtProjectNameProperty = "test";
            vm.txtProjectDescriptionProperty = "test";
            vm.dpStartDateProperty = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

        #endregion empty fields tests

        [Test]
        public void ValidateNewProject_StartDateAfterEndDate()
        {
            vm.txtProjectNameProperty = "test";
            vm.txtProjectDescriptionProperty = "test";
            vm.dpStartDateProperty = DateTime.Now.AddDays(1).ToString();
            vm.dpEndDateProperty = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

        //todo after I've added functions in viewmodel to check that project name doesn't already exist
        [Test]
        [Ignore]
        public void ValidateNewProject_AllFieldsValid_ProjectNameTaken()
        {
            vm.txtProjectNameProperty = "test";//need sample value to test
            vm.txtProjectDescriptionProperty = "test";
            vm.dpStartDateProperty = DateTime.Now.AddDays(1).ToString();
            vm.dpEndDateProperty = DateTime.Now.ToString();

            Assert.IsFalse(vm.DetailsValid());
        }

        [Test]
        public void ValidateNewProject_AllFieldsValid_ProjectNameNotTaken()
        {
            vm.txtProjectNameProperty = "test";
            vm.txtProjectDescriptionProperty = "test";
            vm.dpStartDateProperty = DateTime.Now.ToString();
            vm.dpEndDateProperty = DateTime.Now.AddDays(1).ToString();

            Assert.IsTrue(vm.DetailsValid());
        }

    }
}
