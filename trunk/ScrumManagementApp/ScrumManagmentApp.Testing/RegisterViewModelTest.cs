using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;


namespace ScrumManagmentApp.Testing
{
    [TestFixture]
    public class RegisterViewModelTest
    {
        [Test]
        public void ValidateRegisterTestEmptyForename()
        {
            RegisterViewModel RVM = new RegisterViewModel();

            RVM.Forename = "";
            RVM.Surname = "Smith";
            RVM.Password = "password";
            RVM.Password1 = "password";
            RVM.Email = "Username";


            ;




        }
    }
}
