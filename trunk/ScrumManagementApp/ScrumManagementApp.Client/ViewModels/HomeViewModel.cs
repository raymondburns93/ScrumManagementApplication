using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    /// <summary>
    /// Author: Andrew Baird
    /// View Model for the Home Screen
    /// </summary>
    public class HomeViewModel : ViewModelBase, INestedPageViewModel, IPageViewModel
    {
        #region Variables
        public string Name { get { return "Home View"; } }
        private UserDTO _currentUserDTO { get; set; }

        private ICommand _changeNestedPageCommand;
        private INestedPageViewModel _currentNestedPageViewModel;
        private List<INestedPageViewModel> _nestedPageViewModels;

        public RelayCommand HomeButtonCommand { get; private set; }
        public RelayCommand LogOutButtonCommand { get; private set; }
        public RelayCommand CreateProjectButtonCommand { get; private set; }
        public RelayCommand ViewProjectSummary { get; private set; }

        public int CurrentUserID { get; set; }
        #endregion

        #region Constructor
        public HomeViewModel() { }

        public HomeViewModel(UserDTO user)
        {

            CurrentUserID = user.UserId;
            _currentUserDTO = user;

            HomeButtonCommand = new RelayCommand(NavigateToHome);
            LogOutButtonCommand = new RelayCommand(LogOut);
            CreateProjectButtonCommand = new RelayCommand(CreateProject);

            CurrentNestedPageViewModel = new WelcomeViewViewModel(_currentUserDTO);

            Messenger.Default.Register<INestedPageViewModel>(this, p =>
            {
                CurrentNestedPageViewModel = p;
            });
        }
        #endregion

        #region Properties / Commands
        public ICommand ChangeNestedPageCommand
        {
            get
            {
                if (_changeNestedPageCommand == null)
                {
                    _changeNestedPageCommand = new RelayCommand1(
                        p => ChangeNestedViewModel((INestedPageViewModel)p),
                        p => p is INestedPageViewModel);
                }
                return _changeNestedPageCommand;
            }
        }

        public List<INestedPageViewModel> NestedPageViewModels
        {
            get
            {
                if (_nestedPageViewModels == null)
                    _nestedPageViewModels = new List<INestedPageViewModel>();

                return _nestedPageViewModels;
            }
        }

        public INestedPageViewModel CurrentNestedPageViewModel
        {
            get
            {
                return _currentNestedPageViewModel;
            }
            set
            {
                if (_currentNestedPageViewModel != value)
                {
                    _currentNestedPageViewModel = value;
                    OnPropertyChanged("CurrentNestedPageViewModel");
                }
            }
        }
        #endregion

        #region Methods
        private void ChangeNestedViewModel(INestedPageViewModel viewModel)
        {
            if (!NestedPageViewModels.Contains(viewModel))
                NestedPageViewModels.Add(viewModel);

            CurrentNestedPageViewModel = NestedPageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        public void NavigateToHome()
        {
            WelcomeViewViewModel vm = new WelcomeViewViewModel(_currentUserDTO);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void LogOut()
        {
            LoginViewModel vm = new LoginViewModel();
            Messenger.Default.Send<IPageViewModel>(vm);
        }

        public void CreateProject()
        {
            CreateProjectViewModel vm = new CreateProjectViewModel(_currentUserDTO);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion
    }
}
