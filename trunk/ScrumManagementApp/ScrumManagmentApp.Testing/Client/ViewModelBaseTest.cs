using NUnit.Framework;
using ScrumManagementApp.Client.ViewModels;

namespace ScrumManagmentApp.Testing.Client
{
    [TestFixture]
    public class ViewModelBaseTest
    {
        [Test]
        public void EventHandlerRaisedWhenPropertyIsChanged()
        {
            var obj = new TestViewModelBase();

            bool wasRaised = false;

            obj.PropertyChanged += (sender, e) =>
            {
                Assert.IsTrue(e.PropertyName == "PropertyString");
                wasRaised = true;
            };

            obj.PropertyString = "Test Value";

            if (!wasRaised)
                Assert.Fail("Event Handler wasn't raised when property changed");
        }

        private class TestViewModelBase : ViewModelBase
        {
            private string _propertyString;

            public string PropertyString
            {
                get { return _propertyString; }
                set
                {
                    _propertyString = value;
                    OnPropertyChanged("PropertyString");
                }
            }
        }
    }
}
