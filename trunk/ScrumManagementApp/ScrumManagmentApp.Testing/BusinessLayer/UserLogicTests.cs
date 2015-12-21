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
    public class UserLogicTests
    {
        private User newUser, existingUser, differentUser;

        [SetUp]
        public void SetUp()
        {
            newUser = new User() { UserId=3, Email = "NewUser@test.com", Password = "12345" };
            existingUser = new User() { UserId = 1, Email = "ExistingUser@test.com", Password = "123456" };
            differentUser = new User() { UserId = 2, Email = "DifferentUser@test.com", Password = "1234567" };
            

        }


        [Test]
        public void GetAllUsers_ExistingUsers_ReturnsExistingUsers()
        {

            Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(t => t.GetAll(null)).Returns(existingUser as IList<User>);

            IUserRepository userRepository = mockUserRepo.Object;

            UserLogic userLogic = new UserLogic(userRepository);
            IList<User> result = userLogic.GetAllUsers();
            
            Assert.AreEqual(existingUser as IList<User>,result);

        }

        [Test]
        public void GetUser_AlreadyRegistered_ReturnsUser()
        {
            Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(t => t.GetSingle(It.IsAny<Func<User, bool>>(), null)).Returns(existingUser);


            IUserRepository userRepository = mockUserRepo.Object;

            UserLogic userLogic = new UserLogic(userRepository);
            User result = userLogic.GetUser(existingUser.UserId);
            Assert.AreEqual(existingUser.UserId, result.UserId);
            
        }

        [Test]
        public void AddUser_IsValid_AddsNewUser()
        {

            Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(t => t.Add(It.IsAny<User>()));
            mockUserRepo.Setup(t => t.GetSingle(It.IsAny<Func<User, bool>>(), null)).Returns((User)null);

            IUserRepository userRepository = mockUserRepo.Object;
           

            UserLogic userLogic = new UserLogic(userRepository);
            userLogic.AddUser(newUser);

            mockUserRepo.Verify(t => t.Add(It.IsAny<User>()));
        
        }

        [Test]
        public void GetUserByUsername_IsValid_ReturnsUser()
        {

            Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(t => t.GetSingle(It.IsAny<Func<User, bool>>(), null)).Returns(existingUser);

            IUserRepository userRepository = mockUserRepo.Object;


            UserLogic userLogic = new UserLogic(userRepository);
            User result = userLogic.GetUserByUsername(existingUser.Email);

            Assert.AreEqual(existingUser, result);
        }

        public void GetUser_IsValid_ReturnsUser()
        {

            Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(t => t.GetSingle(It.IsAny<Func<User, bool>>(), null)).Returns(existingUser);

            IUserRepository userRepository = mockUserRepo.Object;

            UserLogic userLogic = new UserLogic(userRepository);
            User result = userLogic.GetUser(existingUser.UserId);

            Assert.AreEqual(existingUser, result);

        }

        [Test]
        public void UpdateUser_IsValid_UpdatesUser()
        {

            Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(t => t.Update(It.IsAny<User>()));

            IUserRepository userRepository = mockUserRepo.Object;

            UserLogic userLogic = new UserLogic(userRepository);
            userLogic.UpdateUser(existingUser);

            mockUserRepo.Verify(t => t.Update(It.IsAny<User>()));

        }

        [Test]
        public void RemoveUser_IsValid_RemovesUser()
        {

            Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(t => t.Remove(It.IsAny<User>()));

            IUserRepository userRepository = mockUserRepo.Object;

            UserLogic userLogic = new UserLogic(userRepository);
            userLogic.RemoveUser(existingUser);

            mockUserRepo.Verify(t => t.Remove(It.IsAny<User>()));

        }



        [Test]
        public void LogInUser_IsAlreadyRegistered_LogsUserIn()
        {
           
            Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(t => t.GetSingle(It.IsAny<Func<User, bool>>(), null)).Returns(existingUser);

            IUserRepository userRepository = mockUserRepo.Object;

            UserLogic userLogic = new UserLogic(userRepository);
            User result = userLogic.LogInUser(existingUser.Email, existingUser.Password);

            Assert.AreEqual(existingUser, result);

        }





    }
}
