using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;
using System;

namespace ScrumManagmentApp.Testing
{
    [TestFixture]
    public class RegisterViewModelTest
    {

        RegisterViewModel rvm;
       
        [SetUp]
        public void Init()
        {
            rvm = new RegisterViewModel();
        }

      
    
        [Test]
        public void TestEmptyForename()
        {

            rvm.Forename = String.Empty;
            rvm.Surname = "test";
            rvm.Email = "test"; //Change when formatting check is added
            rvm.Password = "test";
            rvm.Password1 = "test";
            rvm.Skillset = "test";

            rvm.ValidateRegisterDetails();

            Assert.AreEqual("Forename cannot be empty", rvm.ErrorMessage);

        }          

        [Test]
        public void TestEmptySurname()
        {

            rvm.Forename = "test";
            rvm.Surname = String.Empty;
            rvm.Email = "test"; //Change when formatting check is added
            rvm.Password = "test";
            rvm.Password1 = "test";
            rvm.Skillset = "test";

            rvm.ValidateRegisterDetails();

            Assert.AreEqual("Surname cannot be empty", rvm.ErrorMessage);

        }
        
        [Test]
        public void TestEmptyEmail()
        {

            rvm.Forename = "test";
            rvm.Surname = "test";
            rvm.Email = String.Empty; 
            rvm.Password = "test";
            rvm.Password1 = "test";
            rvm.Skillset = "test";

            rvm.ValidateRegisterDetails();

            Assert.AreEqual("Email cannot be empty", rvm.ErrorMessage);

        }
        
        [Test]
        public void TestEmptyPassword()
        {

            rvm.Forename = "test";
            rvm.Surname = "test";
            rvm.Email = "test"; //Change when formatting check is added
            rvm.Password = String.Empty;
            rvm.Password1 = "test";
            rvm.Skillset = "test";

            rvm.ValidateRegisterDetails();

            Assert.AreEqual("Password cannot be empty", rvm.ErrorMessage);

        }
        
        [Test]
        public void TestEmptyPassword1()
        {

            rvm.Forename = "test";
            rvm.Surname = "test";
            rvm.Email = "test"; //Change when formatting check is added
            rvm.Password = "test";
            rvm.Password1 = String.Empty;
            rvm.Skillset = "test";

            rvm.ValidateRegisterDetails();

            Assert.AreEqual("Passwords don't match", rvm.ErrorMessage);

        }
        
        [Test]
        public void TestEmptySkillset()
        {

            rvm.Forename = "test";
            rvm.Surname = "test";
            rvm.Email = "test"; //Change when formatting check is added
            rvm.Password = "test";
            rvm.Password1 = "test";
            rvm.Skillset = String.Empty;

            rvm.ValidateRegisterDetails();

            Assert.AreEqual("Skillset cannot be empty", rvm.ErrorMessage);

        }

        [Ignore]
        [Test]
        public void TestUsernameExists()
        {

            rvm.Forename = "test";
            rvm.Surname = "test";
            rvm.Email = "test"; //Change when formatting check is added
            rvm.Password = "test";
            rvm.Password1 = "test";
            rvm.Skillset = "test";

            rvm.ValidateRegisterDetails();

            Assert.AreEqual("Email already registered", rvm.ErrorMessage);

        }

        [Ignore]
        [Test]
        public void TestValidEmailFormatting()
        {

            rvm.Forename = "test";
            rvm.Surname = "test";
            rvm.Email = "test"; //Change when formatting check is added
            rvm.Password = "test";
            rvm.Password1 = "test";
            rvm.Skillset = "test";

            rvm.ValidateRegisterDetails();

            Assert.AreEqual("Email already registered", rvm.ErrorMessage); //Change when formatting check is added

        }

        [Ignore]
        [Test]
        public void TestInvalidEmailFormatting()
        {

            rvm.Forename = "test";
            rvm.Surname = "test";
            rvm.Email = "test"; //Change when formatting check is added
            rvm.Password = "test";
            rvm.Password1 = "test";
            rvm.Skillset = "test";

            rvm.ValidateRegisterDetails();

            Assert.AreEqual("Email already registered", rvm.ErrorMessage); //Change when formatting check is added

        }


    }
    
}
