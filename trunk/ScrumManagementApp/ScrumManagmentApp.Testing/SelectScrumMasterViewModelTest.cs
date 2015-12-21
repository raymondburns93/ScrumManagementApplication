using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;
using System;
using System.Collections.Generic;


namespace ScrumManagmentApp.Testing
{
    [TestFixture]
    public class SelectScrumMasterViewModelTest
    {
        SelectScrumMasterViewModel vm;

        [SetUp]
        public void Init()
        {
            vm = new SelectScrumMasterViewModel();
        }

        [Test]
        public void TestSearchTextNotEntered()
        {
            vm.txtSearchProperty = String.Empty;

            Assert.IsFalse(vm.SearchTextEntered());
        }

        [Test]
        public void TestSearchTextEntered()
        {
            vm.txtSearchProperty = "test";
            Assert.IsTrue(vm.SearchTextEntered());
        }

        [Test]
        public void TestInvalidEmailEntered()
        {
            vm.txtSearchProperty = "test";
            vm.SearchForScrumMaster();
            Assert.AreNotEqual(string.Empty, vm.lblErrorMessageProperty);
        }

        [Ignore]
        [Test]
        public void TestValidEmailEntered_UserUnavailable()
        {
            vm.txtSearchProperty = "test";//todo change after functions are added to view model
            vm.SearchForScrumMaster();
            Assert.AreNotEqual(string.Empty, vm.lblErrorMessageProperty);
        }

        [Ignore]
        [Test]
        public void TestValidEmailEntered_UserAvailable()
        {
            vm.txtSearchProperty = "test";//todo change with valid email
            vm.SearchForScrumMaster();
            Assert.AreEqual(string.Empty, vm.lblErrorMessageProperty);
        }

        [Ignore]
        [Test]
        public void TestSelectedUsersStillAvailableAtSaveTime()
        {
            vm.selectedScumMasters = null; //todo get valid users
            vm.ValidateSelectedScrumMasters();
            Assert.AreEqual(string.Empty, vm.lblErrorMessageProperty);
        }

        [Ignore]
        [Test]
        public void TestSelectedUserUnavailableAtSaveTime()
        {
            vm.selectedScumMasters = null; //todo get valid user
            List<ScrumManagementApp.Common.DTOs.UserDTO> originalScrumMasters = vm.selectedScumMasters;
            vm.ValidateSelectedScrumMasters();
            Assert.AreNotEqual(string.Empty, vm.lblErrorMessageProperty);
            Assert.AreNotEqual(originalScrumMasters, vm.selectedScumMasters);
        }

        [Test]
        public void TestDisplaySelectedScrumMasters_UsersSelected()
        {
            vm.selectedScumMasters = new List<ScrumManagementApp.Common.DTOs.UserDTO>();
            ScrumManagementApp.Common.DTOs.UserDTO user = new ScrumManagementApp.Common.DTOs.UserDTO();
            user.Email = "test@email.com";
            vm.selectedScumMasters.Add(user);//todo update when there is good data
            vm.DisplaySelectedScrumMasters();
            Assert.AreNotEqual(string.Empty, vm.lblSelectedScrumMastersProperty);
        }

        [Test]
        public void TestDisplaySelectedScrumMasters_NoUserSelected()
        {
            vm.selectedScumMasters = new List<ScrumManagementApp.Common.DTOs.UserDTO>();
            vm.DisplaySelectedScrumMasters();
            Assert.AreEqual(string.Empty, vm.lblSelectedScrumMastersProperty);
        }
    }
}
