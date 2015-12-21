using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.UserService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    /// <summary>
    /// Author: Andrew Baird
    /// View Model for the Login Screen
    /// </summary>
    public class LoginViewModel : ViewModelBase, IPageViewModel
    {
        #region Constructor
        public LoginViewModel()
        {
            RegisterButtonClickCommand = new RelayCommand(NavigateToRegister);
        }
        #endregion

        #region Private Variables
        private RelayCommand1 _loginCommand;

        private string _emptyFieldMessage = "Ensure all marked fields are completed"; //Displayed if Username or Password field is empty
        private readonly string _emptyString = "";
        private readonly string _requiredField = "*";
        private string _passwordErrorMessage = "";
        private string _usernameErrorMessage = "";
        private string _errorMessage = "";
        private string _username = "";
        private string _password = "";
        #endregion

        #region Getters/Setters
        public RelayCommand RegisterButtonClickCommand { get; private set; }

        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                       (_loginCommand = new RelayCommand1(param => ValidateLoginDetails(), param => true));
            }
        }

        public string Name
        {
            get { return "Login View"; }
        }

        public string PasswordErrorMessage
        {
            get { return _passwordErrorMessage; }
            set
            {
                _passwordErrorMessage = value;
                OnPropertyChanged("PasswordErrorMessage");
            }
        }

        public string UsernameErrorMessage
        {
            get { return _usernameErrorMessage; }
            set
            {
                _usernameErrorMessage = value;
                OnPropertyChanged("UsernameErrorMessage");
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            private get { return _password; }
            set { _password = value; }
        }
        #endregion

        #region Methods
        public void NavigateToRegister()
        {
            RegisterViewModel vm = new RegisterViewModel();
            Messenger.Default.Send<IPageViewModel>(vm);
        }

        private void ValidateLoginDetails()
        {
            bool emptyUsername = (String.IsNullOrWhiteSpace(_username));
            bool emptyPassword = (String.IsNullOrWhiteSpace(_password));

            if (emptyUsername)
            {
                UsernameErrorMessage = _requiredField;
                ErrorMessage = _emptyFieldMessage;
            }
            if (emptyPassword)
            {
                PasswordErrorMessage = _requiredField;
                ErrorMessage = _emptyFieldMessage;
            }
            if (!emptyPassword) { PasswordErrorMessage = _emptyString; }
            if (!emptyUsername) { UsernameErrorMessage = _emptyString; }
            if (!emptyUsername && !emptyPassword)
            {
                ErrorMessage = _emptyString;
                CondensedUserDTO cUser = new CondensedUserDTO
                {
                    Email = _username,
                    Password = _password
                };

                var service = new UserServiceClient();
                UserDTO result = service.ValidateLoginDetails(cUser);
                if (result == null) //user doesn't exist, display error message
                {
                    ErrorMessage = "Invalid Login Details";
                }
                else //user exists, open homescreen
                {
                    Console.WriteLine("LoginView = userID : " + result.UserId);
                    HomeViewModel hvm = new HomeViewModel(result);
                    Messenger.Default.Send<IPageViewModel>(hvm);
                }
            }
        }
        #endregion
    }
}
