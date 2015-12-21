using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.ProjectService;
using ScrumManagementApp.Client.UserService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    public class SelectProductOwnerViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Constructor
        public SelectProductOwnerViewModel(ProjectDTO Project, UserDTO User)
        {
            _projectService = new ProjectServiceClient();
            _userService = new UserServiceClient();
            CurrentProject = Project;
            CurrentUser = User;
        }
        #endregion

        #region Variables
        RelayCommand1 _saveCommand; //event handler for save button
        RelayCommand1 _searchCommand; //event handler for search button

        private String _searchProperty = "";

        private String _selectedProductOwnerProperty = "";

        private String _errorMessageText = "";

        private UserDTO _currentUser { get; set; }

        private ProjectDTO _currentProject { get; set; }

        private const String _NO_AVAILABLE_PRODUCTOWNER_FOUND = "No available product owner found";
        private const String _PRODUCT_OWNER_NO_LONGER_AVAILABLE = "The product owner is no longer available, please select another product owner before saving";

        private const String _HAS_BEEN_ASSIGNED_AS_PRODUCT_OWNER = " has been saved as the product owner for this project";

        private ProjectServiceClient _projectService;
        private UserServiceClient _userService;

        private ICommand _cancelButtonCommand;

        #endregion

        #region Getters/Setters
        public string SearchProperty
        {
            get { return _searchProperty; }
            set { _searchProperty = value; }
        }

        public String SelectedProductOwnerProperty //get/set text property for selected product owner label in view
        {
            get { return _selectedProductOwnerProperty; }
            set
            {
                _selectedProductOwnerProperty = value;
                OnPropertyChanged("SelectedProductOwnerProperty");
            }
        }

        public UserDTO selectedProductOwner { get; set; }

        public UserDTO CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public ProjectDTO CurrentProject
        {
            get { return _currentProject; }
            set { _currentProject = value; }
        }

        public string lblErrorMessageProperty   //get/set text property for error message in view
        {
            get
            {
                return _errorMessageText;
            }
            set
            {
                if (_errorMessageText != value)
                {
                    _errorMessageText = value;
                    OnPropertyChanged("lblErrorMessageProperty");
                }
            }
        }

        public ICommand SaveCommand //event handler for save button, please do not rename
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand1(param => ValidateSelectedProductOwner(), param => true);
                }
                return _saveCommand;
            }
        }

        public ICommand SearchCommand //event handler for search button in the search view, please do not rename
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand1(param => SearchForProductOwner(), param => SearchTextEntered());
                }
                return _searchCommand;
            }
        }

        public string Name
        {
            get { return "Select Product Owner View"; }
        }

        public ICommand CancelButtonCommand
        {
            get
            {
                return _cancelButtonCommand ??
                    (_cancelButtonCommand = new RelayCommand1(param => ProjectSummary()));
            }
        }
        #endregion

        #region Methods
        public void ValidateSelectedProductOwner()//check none of the product owners have been assigned to project since search was done
        {
            if (selectedProductOwner != null)
            {
                //check avaialbility of product owner
                bool HasConflictingProjects = _projectService.HasConflictingProjects(selectedProductOwner.UserId, CurrentProject.StartDate, CurrentProject.EndDate);

                if (!HasConflictingProjects)
                {
                    //save product owner against project
                    _projectService.AssignProductOwnerToProject(CurrentProject, selectedProductOwner.UserId);
                    lblErrorMessageProperty = selectedProductOwner.Email + _HAS_BEEN_ASSIGNED_AS_PRODUCT_OWNER;
                }
                else
                {
                    selectedProductOwner = null;
                    lblErrorMessageProperty = _PRODUCT_OWNER_NO_LONGER_AVAILABLE;
                    SelectedProductOwnerProperty = String.Empty;
                }
            }
            else
            {
                lblErrorMessageProperty = "Search for a user before saving";
            }
        }

        public bool SearchTextEntered()
        {
            return (_searchProperty.Trim() != String.Empty);
        }

        public void SearchForProductOwner()
        {
            UserDTO foundProductOwner = _userService.GetUserByEmail(_searchProperty, pReturnScrumMaster: false, pReturnProductOwner: true);

            if (foundProductOwner == null)
            {
                lblErrorMessageProperty = _NO_AVAILABLE_PRODUCTOWNER_FOUND;
            }
            else
            {
                //check avaialbility of product owner

                bool hasConflictingProjects = _projectService.HasConflictingProjects(foundProductOwner.UserId, CurrentProject.StartDate, CurrentProject.EndDate);

                if (!hasConflictingProjects)
                {
                    selectedProductOwner = foundProductOwner;
                    lblErrorMessageProperty = string.Empty;
                    DisplaySelectedProductOwner();
                }
                else
                {
                    lblErrorMessageProperty = _NO_AVAILABLE_PRODUCTOWNER_FOUND;
                }
            }
        }

        public void DisplaySelectedProductOwner()
        {
            if (selectedProductOwner != null)
            {
                SelectedProductOwnerProperty = "You have selected: " + selectedProductOwner.Email;//todo change with user email
            }
            else
            {
                SelectedProductOwnerProperty = String.Empty;
            }
        }

        public void ProjectSummary()
        {
            ProjectSummaryViewModel vm = new ProjectSummaryViewModel(CurrentProject, CurrentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion


    }
}
