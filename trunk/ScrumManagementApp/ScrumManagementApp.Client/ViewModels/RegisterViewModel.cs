using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    /// View Model for the Register View
    /// </summary>
    public class RegisterViewModel : ViewModelBase, IPageViewModel
    {
        #region Constructor
        public RegisterViewModel()
        {
            CancelButtonClickCommand = new RelayCommand(NavigateToLogin);
        }
        #endregion

        #region Private Variables
        private readonly Dictionary<string, ICollection<string>> _validationErrors = new Dictionary<string, ICollection<string>>();
        private string _forename = "";
        private string _surname = "";
        private string _email = "";
        private string _password = "";
        private string _password1 = "";
        private string _skillset = "";
        private bool _isProductOwner;
        private bool _isScrumMaster;
        private bool _isDeveloper;
        private string _errorMessage = "";
        private RelayCommand1 _registerCommand;

        #endregion

        #region Getters/Setters
        public ICommand RegisterButtonClickCommand { get; private set; }

        public RelayCommand CancelButtonClickCommand { get; private set; }

        public ICommand RegisterCommand //event handler for register button, please do not rename
        {
            get
            {
                if (_registerCommand == null)
                {
                    _registerCommand = new RelayCommand1(param => ValidateRegisterDetails(), param => true);
                }
                return _registerCommand;
            }
        }

        public string Forename
        {
            get { return _forename; }
            set { _forename = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Password1
        {
            get { return _password1; }
            set { _password1 = value; }
        }

        public string Skillset
        {
            get { return _skillset; }
            set { _skillset = value; }
        }

        public bool IsProductOwner
        {
            get { return _isProductOwner; }
            set { _isProductOwner = value; Console.Write("Product owner = " + value); }
        }

        public bool IsScrumMaster
        {
            get { return _isScrumMaster; }
            set { _isScrumMaster = value; Console.Write("Scrum Master = " + value); }
        }

        public bool IsDeveloper
        {
            get { return _isDeveloper; }
            set { _isDeveloper = value; Console.Write("Developer = " + value); }
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

        public string Name
        {
            get { return "Register View"; }
        }
        #endregion

        #region Methods
        public void NavigateToLogin()
        {
            //View to change to
            LoginViewModel vm = new LoginViewModel();

            //Send the navigation Message
            Messenger.Default.Send<IPageViewModel>(vm);
        }
        
        public void ValidateRegisterDetails()
        {
            var service = new UserServiceClient();
            bool valid = true;

            if (String.IsNullOrWhiteSpace(_forename))
            {
                ErrorMessage = "Forename cannot be empty";
                valid = false;
            }
            if (String.IsNullOrWhiteSpace(_surname))
            {
                ErrorMessage = "Surname cannot be empty";
                valid = false;
            }
            if (String.IsNullOrWhiteSpace(_email))
            {
                ErrorMessage = "Email cannot be empty";
                valid = false;
            }
            if (!Regex.IsMatch(_email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase))
            {
                ErrorMessage = "Enter a valid email address";
                valid = false;
            }

            if (String.IsNullOrWhiteSpace(_password))
            {
                ErrorMessage = "Password cannot be empty";
                valid = false;
            }
            if (!_password.Equals(_password1))
            {
                ErrorMessage = "Passwords don't match";
                valid = false;
            }
            if (String.IsNullOrWhiteSpace(_skillset))
            {
                ErrorMessage = "Skillset cannot be empty";
                valid = false;
            }
            if (service.UsernameExists(_email))
            {
                ErrorMessage = "Email already registered";
                valid = false;
            }

            if (valid)
            {
                UserDTO user = new UserDTO();

                user.Firstname = _forename;
                user.Lastname = _surname;
                user.Email = _email;
                user.Password = _password;
                user.SkillSet = _skillset;
                user.IsDeveloper = _isDeveloper;
                user.IsProductOwner = _isProductOwner;
                user.IsScrumMaster = _isScrumMaster;

                service.CreateUser(user);

                ErrorMessage = "Registration Complete!";
            }
        }

        protected virtual void OnDispose()
        { }

        public void Dispose()
        {
            OnDispose();
        }
        #endregion

    }
}