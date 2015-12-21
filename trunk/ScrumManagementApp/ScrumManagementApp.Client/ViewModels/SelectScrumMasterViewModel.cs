using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.ProjectService;
using ScrumManagementApp.Client.UserService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    public class SelectScrumMasterViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Constructor
        public SelectScrumMasterViewModel(ProjectDTO Project, UserDTO User)
        {
            _projectService = new ProjectServiceClient();
            _userService = new UserServiceClient();
            CurrentProject = Project;
            CurrentUser = User;
        }

        #endregion

        #region Variables
        private UserDTO _currentUser { get; set; }
        private ProjectDTO _currentProject { get; set; }

        RelayCommand1 _saveCommand; //event handler for save button
        RelayCommand1 _searchCommand; //event handler for search button

        private String _enteredSearchText = "";
        private String _selectedScrumMasterLabelText = "";
        private String _errorMessageText = "";

        private ProjectServiceClient _projectService = new ProjectServiceClient();
        private UserServiceClient _userService = new UserServiceClient();

        private const String _NO_AVAILABLE_SCRUMMASTERS_FOUND = "No available scrum masters found";
        private const String _SCRUM_MASTER_ALREADY_SELECTED = "This scrum master has already been selected";
        private const String _SCRUM_MASTERS_ARE_NO_LONGER_AVAILABLE = "One or more of the scrum masters are no longer available, please review changes before saving";
        private const String _HAS_BEEN_ASSIGNED_AS_PRODUCT_OWNER = "Selected users have been saved as scrum masters for this project";
        private const String _CONFLICTING_PROJECT = "The person is unavailable due to a conflicting project";

        private ICommand _cancelButtonCommand;

        #endregion

        #region Getters/Setters
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

        public List<UserDTO> selectedScumMasters = new List<UserDTO>();
        public string SearchProperty   //get/set text property for search text textbox in view
        {
            get
            {
                return _enteredSearchText;
            }
            set
            {
                if (_enteredSearchText != value)
                {
                    _enteredSearchText = value;
                }
            }
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

        public String lblSelectedScrumMastersProperty //get/set text property for selected scrum master label in view
        {
            get
            {
                return _selectedScrumMasterLabelText;
            }
            set
            {
                if (_selectedScrumMasterLabelText != value)
                {
                    _selectedScrumMasterLabelText = value;
                    OnPropertyChanged("lblSelectedScrumMastersProperty");
                }
            }
        }

        public ICommand SaveCommand //event handler for save button, please do not rename
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand1(param => ValidateSelectedScrumMasters(), param => true);
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
                    _searchCommand = new RelayCommand1(param => SearchForScrumMaster(), param => SearchTextEntered());
                }
                return _searchCommand;
            }
        }

        public ICommand CancelButtonCommand
        {
            get
            {
                return _cancelButtonCommand ??
                    (_cancelButtonCommand = new RelayCommand1(param => ProjectSummary()));
            }
        }

        public string Name
        {
            get { return "Select Scrum Master View"; }
        }
        #endregion

        #region Methods
        public void ValidateSelectedScrumMasters()//check none of the scrum masters have been assigned to project since search was done
        {
            bool availabiltyChanged = false;
            List<UserDTO> newAvailableScrumMasters = selectedScumMasters;

            if (newAvailableScrumMasters.Count == 0 || newAvailableScrumMasters == null)
            {
                lblErrorMessageProperty = "Please select a valid user";

            }
            else
            {

                foreach (UserDTO user in selectedScumMasters.ToList())
                {
                    bool userUnavailable = false;

                    //check avaialbility of product owner
                    userUnavailable = _projectService.HasConflictingProjects(user.UserId, CurrentProject.StartDate, CurrentProject.EndDate);

                    if (userUnavailable)
                    {
                        availabiltyChanged = true;
                        newAvailableScrumMasters.Remove(user);
                    }
                }

                if (availabiltyChanged)
                {
                    selectedScumMasters = newAvailableScrumMasters;
                    lblErrorMessageProperty = _SCRUM_MASTERS_ARE_NO_LONGER_AVAILABLE;
                    DisplaySelectedScrumMasters();
                }
                else
                {
                    Int32 length = selectedScumMasters.Count;
                    Int32[] scrumMasterIDs = new Int32[length];
                    for (var i = 0; i < length; i++)
                    {
                        scrumMasterIDs[i] = selectedScumMasters[i].UserId;
                    }
                    _projectService.AssignsScrumMastersToProject(CurrentProject, scrumMasterIDs);
                    lblErrorMessageProperty = _HAS_BEEN_ASSIGNED_AS_PRODUCT_OWNER;
                }
            }
        }

        public bool SearchTextEntered()
        {
            return (_enteredSearchText.Trim() != String.Empty);
        }

        public void SearchForScrumMaster()
        {

            UserDTO foundScrumMaster =
                _userService.GetUserByEmail(_enteredSearchText, pReturnScrumMaster: true, pReturnProductOwner: false);
            bool HasConflictingProjects = false;

            if (foundScrumMaster == null)
            {
                lblErrorMessageProperty = _NO_AVAILABLE_SCRUMMASTERS_FOUND;
            }
            else
            {
                HasConflictingProjects = _projectService.HasConflictingProjects(foundScrumMaster.UserId, CurrentProject.StartDate, CurrentProject.EndDate);

                if (HasConflictingProjects)
                {
                    lblErrorMessageProperty = _CONFLICTING_PROJECT;
                }
                else if (selectedScumMasters.Contains(foundScrumMaster))
                {
                    lblErrorMessageProperty = _SCRUM_MASTER_ALREADY_SELECTED;
                }
                else
                {
                    selectedScumMasters.Add(foundScrumMaster);
                    lblErrorMessageProperty = string.Empty;
                    DisplaySelectedScrumMasters();
                }
            }

        }

        public void DisplaySelectedScrumMasters()
        {
            if (selectedScumMasters.Count() == 0)
            {
                lblSelectedScrumMastersProperty = String.Empty;
            }
            else
            {
                lblSelectedScrumMastersProperty = String.Empty;
                foreach (var user in selectedScumMasters)
                {
                    lblSelectedScrumMastersProperty += user.Email + "\n";
                }
                lblSelectedScrumMastersProperty.Trim();
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
