using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;
using ScrumManagementApp.Common.DTOs;
using System;


namespace ScrumManagmentApp.Testing.Client
{
    [TestFixture]
    public class SelectProductOwnerViewModelTest
    {
        SelectProductOwnerViewModel vm;
        ProjectDTO dto;
        UserDTO udto;

        [SetUp]
        public void Init()
        {
            dto = new ProjectDTO();
            udto = new UserDTO();
            vm = new SelectProductOwnerViewModel(dto, udto);
        }

        [Test]
        public void TestSearchTextNotEntered()
        {
            vm.SearchProperty = String.Empty;

            Assert.IsFalse(vm.SearchTextEntered());
        }

        [Test]
        public void TestSearchTextEntered()
        {
            vm.SearchProperty = "test";

            Assert.IsTrue(vm.SearchTextEntered());
        }

        [Test]
        public void TestInvalidEmailEntered()
        {
            vm.SearchProperty = "test";
            vm.SearchForProductOwner();
            Assert.AreNotEqual(string.Empty, vm.lblErrorMessageProperty);
        }

        [Ignore]
        [Test]
        public void TestValidEmailEntered_UserUnavailable()
        {
            vm.SearchProperty = "test";//todo change after functions are added to view model
            vm.SearchForProductOwner();
            Assert.AreNotEqual(string.Empty, vm.lblErrorMessageProperty);
        }

        [Ignore]
        [Test]
        public void TestValidEmailEntered_UserAvailable()
        {
            vm.SearchProperty = "test";//todo change with valid email
            vm.SearchForProductOwner();
            Assert.AreEqual(string.Empty, vm.lblErrorMessageProperty);
        }

        [Ignore]
        [Test]
        public void TestSelectedUserStillAvailableAtSaveTime()
        {
            vm.selectedProductOwner = null; //todo get valid user
            vm.ValidateSelectedProductOwner();
            Assert.AreEqual(string.Empty, vm.lblErrorMessageProperty);
        }

        [Ignore]
        [Test]
        public void TestSelectedUserUnavailableAtSaveTime()
        {
            vm.selectedProductOwner = null; //todo get valid user
            vm.ValidateSelectedProductOwner();
            Assert.AreNotEqual(string.Empty, vm.lblErrorMessageProperty);
            Assert.IsNull(vm.selectedProductOwner);
        }

        [Test]
        public void TestDisplaySelectedProductOwner_UserSelected()
        {
            vm.selectedProductOwner = new ScrumManagementApp.Common.DTOs.UserDTO();
            vm.selectedProductOwner.Email = "test@email.com";
            vm.DisplaySelectedProductOwner();
            Assert.AreNotEqual(string.Empty, vm.SelectedProductOwnerProperty);
        }

        [Test]
        public void TestDisplaySelectedProductOwner_NoUserSelected()
        {
            vm.selectedProductOwner = null;
            vm.DisplaySelectedProductOwner();
            Assert.AreEqual(string.Empty, vm.SelectedProductOwnerProperty);
        }
    }
}
