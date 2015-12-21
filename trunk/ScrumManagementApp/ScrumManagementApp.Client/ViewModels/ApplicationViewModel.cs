using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;

namespace ScrumManagementApp.Client.ViewModels
{
    class ApplicationViewModel : ViewModelBase
    {
        #region Constructor
        public ApplicationViewModel()
        {
            // Add available pages
            //TODO - AB - remove??
            //PageViewModels.Add(new LoginViewModel());
            //PageViewModels.Add(new RegisterViewModel());
            //PageViewModels.Add(new HomeViewModel());

            // Set starting page
            //CurrentPageViewModel = PageViewModels[0];
            CurrentPageViewModel = new LoginViewModel();

            Messenger.Default.Register<IPageViewModel>(this, p =>
            {
                CurrentPageViewModel = p;
            });
        }
        #endregion

        #region Fields
        private ICommand _changePageCommand;
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;
        #endregion

        #region Properties / Commands
        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand1(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }
        #endregion

        #region Methods
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }
        #endregion
    }
}
