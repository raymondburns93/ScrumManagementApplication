using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;
using System;


namespace ScrumManagmentApp.Testing
{
    [TestFixture]
    public class SelectProductOwnerViewModelTest
    {
        SelectProductOwnerViewModel vm;

        [SetUp]
        public void Init()
        {
            vm = new SelectProductOwnerViewModel();
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
            vm.SearchForProductOwner();
            Assert.AreNotEqual(string.Empty, vm.lblErrorMessageProperty);
        }

        [Ignore]
        [Test]
        public void TestValidEmailEntered_UserUnavailable()
        {
            vm.txtSearchProperty = "test";//todo change after functions are added to view model
            vm.SearchForProductOwner();
            Assert.AreNotEqual(string.Empty, vm.lblErrorMessageProperty);
        }

        [Ignore]
        [Test]
        public void TestValidEmailEntered_UserAvailable()
        {
            vm.txtSearchProperty = "test";//todo change with valid email
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
            Assert.AreNotEqual(string.Empty, vm.lblSelectedProductOwnerProperty);
        }

        [Test]
        public void TestDisplaySelectedProductOwner_NoUserSelected()
        {
            vm.selectedProductOwner = null;
            vm.DisplaySelectedProductOwner();
            Assert.AreEqual(string.Empty, vm.lblSelectedProductOwnerProperty);
        }
    }
}
