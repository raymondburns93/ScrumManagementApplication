using System.ComponentModel;

namespace ScrumManagementApp.Client.ViewModels
{
    /// <summary>
    /// Base class which all ViewModels will derive from
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string pPropertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(pPropertyName);
                handler(this, e);
            }
        }

    }
}
