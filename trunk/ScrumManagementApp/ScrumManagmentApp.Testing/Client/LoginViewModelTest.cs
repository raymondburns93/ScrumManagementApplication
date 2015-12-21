using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;

namespace ScrumManagmentApp.Testing.Client
{
    [TestFixture]
    public class LoginViewModelTest
    {
        LoginViewModel vm;
        string testString;

        [SetUp]
        public void SetupViewModel()
        {
            vm = new LoginViewModel();
            testString = "Test";
        }

        [Test]
        public void VerifyName()
        {
            string temp = vm.Name;
            Assert.AreEqual("Login View", "Login View");
        }

        [Test]
        public void VerifyPasswordErrorMessageSetGet()
        {
            vm.PasswordErrorMessage = testString;
            Assert.AreEqual(testString, vm.PasswordErrorMessage);
        }

        [Test]
        public void VerifyUsernameErrorMessageSetGet()
        {
            vm.UsernameErrorMessage = testString;
            Assert.AreEqual(testString, vm.UsernameErrorMessage);
        }

        [Test]
        public void VerifyErrorMessageSetGet()
        {
            vm.ErrorMessage = testString;
            Assert.AreEqual(testString, vm.ErrorMessage);
        }

        [Test]
        public void VerifyUsernameSetGet()
        {
            vm.Username = testString;
            Assert.AreEqual(testString, vm.Username);
        }

    }
}
