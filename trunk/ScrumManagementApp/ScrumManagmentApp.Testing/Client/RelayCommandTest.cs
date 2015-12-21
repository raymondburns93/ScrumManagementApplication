using System;
using NUnit.Framework;
using ScrumManagementApp.Client.Helpers;

namespace ScrumManagmentApp.Testing.Client
{
    [TestFixture]
    public class RelayCommandTest
    {

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsExceptionIfActionParameterIsNull()
        {
            var command = new RelayCommand1(null);
        }

        [Test]
        public void CanExecuteIsTrueByDefault()
        {
            var command = new RelayCommand1(obj => { });
            Assert.IsTrue(command.CanExecute(null));
        }

        //TODO - Add test for Execute

    }
}
