using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.ProjectService;
using ScrumManagementApp.Client.SprintService;
using ScrumManagementApp.Client.UserService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    /// <summary>
    /// Author: Raymond Burns
    /// Extended by James Rainey
    /// Description: View Model for sprint management view
    /// </summary>
    public class SprintManagementViewModel : ViewModelBase, INestedPageViewModel
    {

        #region Variables

        private ProjectServiceClient _projectService = new ProjectServiceClient();
        private UserServiceClient _userService = new UserServiceClient();
        private SprintServiceClient _sprintService = new SprintServiceClient();

        private Boolean _isUserScrumMaster { get; set; }

        public RelayCommand SprintBacklogButtonCommand { get; private set; }

        private ICommand _showSprintBacklogCommand;

        private ICommand _backCommand;

        RelayCommand1 _searchCommand;
        RelayCommand1 _selectCommand;

        private String _enteredSearchText = "";
        private String _selectedDevelopersLabel = "";
        private String _errorMessageText = "";

        private List<UserDTO> developersSearchResults;

        private const String _NO_AVAILABLE_DEVELOPERS_FOUND = "No available developers found.";
        private const String _DEVELOPER_ALREADY_SELECTED = "This developer has already been selected.";
        private const String _DEVELOPER_ALREADY_ASSIGNED = "This developer has already been assigned.";

        private ICommand _setDeveloperListCommand;

        private UserDTO _currentUser { get; set; }
        public UserDTO CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        private ProjectDTO _currentProject { get; set; }

        private SprintDTO _currentSprint { get; set; }

        private string _sprintName = "";

        private DateTime _sprintStartDate;

        private DateTime? _sprintEndDate;

        private UserDTO _sprintScrumMaster { get; set; }

        private string _sprintScrumMasterName = "";

        private List<UserDTO> _sprintDevelopers { get; set; }

        private string _sprintDevelopersNames = "";
        #endregion

        #region Getters/Setters
        public Boolean IsUserScrumMaster
        {
            get { return _isUserScrumMaster; }
            set { _isUserScrumMaster = value; }

        }

        // Show if the user is of type ScrumMaster
        public Visibility ShowIfScrumMaster
        {
            get { return IsUserScrumMaster ? Visibility.Visible : Visibility.Collapsed; }
        }

        public ICommand ShowSprintBacklogCommand
        {
            get
            {
                return _showSprintBacklogCommand ??
                    (_showSprintBacklogCommand = new RelayCommand1(param => ViewSprintBacklog(), param => true));
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ??
                (_backCommand = new RelayCommand1(param => PreviousView(), param => true));
            }
        }


        public string Name
        {
            get { return "Sprint Management View"; }
        }

        public List<UserDTO> selectedDevelopers = new List<UserDTO>();

        /// <summary>
        ///  Get/set text property for search textbox
        /// </summary>
        public string SearchProperty
        {
            get { return _enteredSearchText; }
            set
            {
                if (_enteredSearchText != value)
                    _enteredSearchText = value;
            }
        }

        public List<UserDTO> DeveloperSearchResults
        {
            get { return developersSearchResults; }
            set
            {
                developersSearchResults = value;
                OnPropertyChanged("DeveloperSearchResults");
            }
        }

        /// <summary>
        /// Get/Set text for error message
        /// </summary>
        public string ErrorMessageText
        {
            get { return _errorMessageText; }
            set
            {
                if (_errorMessageText != value)
                {
                    _errorMessageText = value;
                    OnPropertyChanged("ErrorMessageText");
                }
            }
        }

        /// <summary>
        /// Get/Set text for selected developer label
        /// </summary>
        public string SelectedDevelopersLabel
        {
            get { return _selectedDevelopersLabel; }
            set
            {
                if (_selectedDevelopersLabel != value)
                    _selectedDevelopersLabel = value;
            }
        }

        /// <summary>
        ///  Event Handler for search button
        /// </summary>
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand1(param => SearchForDevelopers(), param => SearchTextEntered());
                }
                return _searchCommand;
            }
        }

        /// <summary>
        ///   Event Handler for select Button
        /// </summary>
        public ICommand SelectCommand
        {
            get
            {
                if (_selectCommand == null)
                {
                    _selectCommand = new RelayCommand1(param => SelectDeveloper(param));
                }
                return _selectCommand;
            }
        }


        public ICommand SetDeveloperListCommand
        {
            get
            {
                return _setDeveloperListCommand ??
                    (_setDeveloperListCommand = new RelayCommand1(AddDeveloperToList));
            }
        }

        public ProjectDTO CurrentProject
        {
            get { return _currentProject; }
            set { _currentProject = value; }
        }

        public SprintDTO CurrentSprint
        {
            get { return _currentSprint; }
            set { _currentSprint = value; }
        }

        public string SprintName
        {
            get { return _sprintName; }
            set { _sprintName = value; }

        }

        public DateTime SprintStartDate
        {
            get { return _sprintStartDate; }
            set { _sprintStartDate = value; }

        }

        public DateTime? SprintEndDate
        {
            get { return _sprintEndDate; }
            set { _sprintEndDate = value; }

        }


        public string SprintScrumMasterName
        {
            get { return _sprintScrumMasterName; }
            set { _sprintScrumMasterName = value; }

        }

        public string SprintDevelopersNames
        {
            get { return _sprintDevelopersNames; }
            set
            {
                _sprintDevelopersNames = value;
                OnPropertyChanged("SprintDevelopersNames");
            }

        }

        #endregion

        #region Constructor
        public SprintManagementViewModel(SprintDTO sprintDTO, UserDTO currentUser, ProjectDTO currentProject)
        {
            //SprintDTO STO;
            SprintBacklogButtonCommand = new RelayCommand(NavigateToSprintBacklog);

            _currentSprint = sprintDTO;
            _sprintName = sprintDTO.SprintName;
            _sprintStartDate = sprintDTO.StartDate;
            _sprintEndDate = sprintDTO.EndDate;
            _sprintScrumMaster = _sprintService.GetScrumManagerForSprint(sprintDTO.SprintId);

            if (_sprintScrumMaster != null)
            {
                _sprintScrumMasterName = _sprintScrumMaster.Firstname + " " + _sprintScrumMaster.Lastname;
            }

            DisplayDeveloperNames();

            CurrentUser = currentUser;
            CurrentProject = currentProject;

            IsUserScrumMaster = _projectService.IsScrumMaster(currentProject, currentUser.UserId);
        }
        #endregion

        #region Methods

        /// <summary>
        ///  Check if text has been entered in the search box
        /// </summary>
        public bool SearchTextEntered()
        {
            return (_enteredSearchText.Trim() != String.Empty);
        }

        /// <summary>
        ///  Search for developers that match the search criteria
        /// </summary>
        public void SearchForDevelopers()
        {
            UserDTO[] foundDevelopers = _userService.FindUsersBySkillset(_enteredSearchText);
            DeveloperSearchResults = new List<UserDTO>(foundDevelopers);
        }

        /// <summary>
        /// Adds a developer to the list of selected developers and assigns them to the sprint.
        /// </summary>
        /// <param name="Dev"></param>
        public void SelectDeveloper(object Dev)
        {
            foreach (UserDTO dev in selectedDevelopers)
            {
                _sprintService.AssignDeveloperToSprint(_currentSprint, dev.UserId);
            }

            selectedDevelopers.Clear();
            DisplayDeveloperNames();
        }

        public void DisplayDeveloperNames()
        {
            _sprintDevelopers = _sprintService.GetDevelopersForSprint(_currentSprint.SprintId);

            if (_sprintDevelopers != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (UserDTO developer in _sprintDevelopers)
                {
                    sb.Append(developer.Firstname + " " + developer.Lastname + " ");
                }
                SprintDevelopersNames = sb.ToString();
            }
        }

        /// <summary>
        ///  Display the selected developers in the view
        /// </summary>
        public void DisplaySelectedDevelopers()
        {
            if (selectedDevelopers.Count == 0)
            {
                SelectedDevelopersLabel = String.Empty;
            }
            else
            {
                foreach (var dev in selectedDevelopers)
                {
                    SelectedDevelopersLabel += dev.Email + dev.SkillSet + "\n";
                }
                SelectedDevelopersLabel.Trim();
            }
        }

        public void AddDeveloperToList(object developer)
        {
            UserDTO dev = developer as UserDTO;

            // Add dev to list of developers if they're not already in the list
            // else do nothing

            List<UserDTO> devsAlreadyAssigned = _sprintService.GetDevelopersForSprint(_currentSprint.SprintId);

            if (devsAlreadyAssigned.Exists(d => d.UserId == dev.UserId))
            {
                ErrorMessageText = _DEVELOPER_ALREADY_ASSIGNED;
                return;
            }
            if (!selectedDevelopers.Contains(dev))
            {
                selectedDevelopers.Add(dev);
            }

            // On save developers button, save the list
        }

        public void ViewSprintBacklog()
        {
            SprintBacklogViewerViewModel vm = new SprintBacklogViewerViewModel(_currentSprint, _currentProject, _currentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void NavigateToSprintBacklog()
        {
            SprintBacklogViewModel vm = new SprintBacklogViewModel(_currentProject, _currentSprint, _currentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }


        public void PreviousView()
        {
            SprintSummaryViewModel vm = new SprintSummaryViewModel(_currentSprint, _currentUser, _currentProject);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion
    }
}
