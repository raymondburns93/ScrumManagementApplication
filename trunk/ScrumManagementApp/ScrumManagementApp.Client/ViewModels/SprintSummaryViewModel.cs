using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.ProjectService;
using ScrumManagementApp.Client.SprintService;
using ScrumManagementApp.Client.UserService;
using ScrumManagementApp.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ScrumManagementApp.Client.ViewModels
{
    public class SprintSummaryViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Variables
        private ProjectServiceClient _projectService = new ProjectServiceClient();
        private SprintServiceClient _sprintService = new SprintServiceClient();

        private List<UserDTO> _developerList;

        private Boolean _isUserScrumMaster { get; set; }

        private Boolean _isUserDeveloper { get; set; }
        
        private SprintDTO _currentSprint { get; set; }
        public SprintDTO CurrentSprint
        {
            get { return _currentSprint; }
            set { _currentSprint = value; }
        }

        private UserDTO _currentUser { get; set; }

        private ProjectDTO _currentProject { get; set; }

        private string _sprintName { get; set; }

        private UserDTO _sprintScrumMaster { get; set; }
        private string _sprintScrumMasterName = "";

        private List<UserDTO> _sprintDevelopers { get; set; }

        private string _sprintDevelopersNames = "";

        private DateTime _sprintStartDate { get; set; }

        private DateTime? _sprintEndDate { get; set; }

        private ICommand _showSprintBacklogCommand;
        private ICommand _addTasksToSprint;

        private ICommand _backCommand;

        private ICommand _viewTasksCommand;

        private ICommand _showSprintManagementCommand;

        // Show if the user is of type
        //public Visibility ShowIfProductOwner
        //{
        //    get { return _isUserProductOwner ? Visibility.Visible : Visibility.Collapsed; }
        //}
        #endregion

        #region Getters/Setters
        public string Name
        {
            get { return "SprintSummaryView"; }
        }

        public Boolean IsUserScrumMaster
        {
            get { return _isUserScrumMaster; }
            set { _isUserScrumMaster = value; }

        }

        public Boolean IsUserDeveloper
        {
            get { return _isUserDeveloper; }
            set
            {
                _isUserDeveloper = value;
                OnPropertyChanged("IsUserDeveloper");
            }

        }

        // Show if the user is of type ScrumMaster
        public Visibility ShowIfScrumMaster
        {
            get { return IsUserScrumMaster ? Visibility.Visible : Visibility.Collapsed; }
        }

        //Show if the user is of type Developer
        public Visibility ShowIfDeveloper
        {
            get { return IsUserDeveloper ? Visibility.Visible : Visibility.Collapsed; }
        }

        public RelayCommand SprintBacklogButtonCommand { get; private set; }

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

        public string SprintName
        {
            get { return _sprintName; }
            set { _sprintName = value; }
        }

        public string SprintScrumMasterName
        {
            get { return _sprintScrumMasterName; }
            set
            {
                _sprintScrumMasterName = value;
                OnPropertyChanged("SprintScrumMasterName");
            }

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

        public DateTime SprintStartDate
        {
            get { return _sprintStartDate; }
            set
            {
                _sprintStartDate = value;
                OnPropertyChanged("SprintStartDate");
            }

        }


        public DateTime? SprintEndDate
        {
            get { return _sprintEndDate; }
            set
            {
                _sprintEndDate = value;
                OnPropertyChanged("SprintEndDate");
            }

        }

        public ICommand ShowSprintBacklogCommand
        {
            get
            {
                return _showSprintBacklogCommand ??
                    (_showSprintBacklogCommand = new RelayCommand1(param => ViewSprintBacklog(), param => true));
            }
        }

        public ICommand AddTasksToSprint
        {
            get
            {
                return _addTasksToSprint ??
                    (_addTasksToSprint = new RelayCommand1(param => ViewAddTasksView(), param => true));
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

        public ICommand ViewTasksCommand
        {
            get
            {
                return _viewTasksCommand ??
                (_viewTasksCommand = new RelayCommand1(param => TasksView(), param => true));
            }
        }

        public ICommand ShowSprintManagementCommand
        {
            get
            {
                return _showSprintManagementCommand ??
                    (_showSprintManagementCommand = new RelayCommand1(param => ViewSprintManagement(), param => true));
            }
        }
        #endregion

        #region Constructor
        public SprintSummaryViewModel(SprintDTO sprintDTO, UserDTO currentUser, ProjectDTO currentProject)
        {
            SprintBacklogButtonCommand = new RelayCommand(NavigateToSprintBacklog);

            CurrentSprint = sprintDTO;
            SprintName = sprintDTO.SprintName;
            SprintStartDate = sprintDTO.StartDate;
            SprintEndDate = sprintDTO.EndDate;
            _sprintScrumMaster = _sprintService.GetScrumManagerForSprint(sprintDTO.SprintId);
            _sprintDevelopers = _sprintService.GetDevelopersForSprint(sprintDTO.SprintId);

            if (_sprintScrumMaster != null)
            {
                SprintScrumMasterName = _sprintScrumMaster.Firstname + " " + _sprintScrumMaster.Lastname;
            }

            if (_sprintDevelopers != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (UserDTO developer in _sprintDevelopers)
                {
                    sb.Append(developer.Firstname + " " + developer.Lastname + " ");
                }
                SprintDevelopersNames = sb.ToString();
            }

            CurrentUser = currentUser;
            CurrentProject = currentProject;

            IsUserScrumMaster = _projectService.IsScrumMaster(currentProject, currentUser.UserId);
            _developerList = _sprintService.GetDevelopersForSprint(_currentSprint.SprintId);
            IsUserDeveloper = _developerList.Exists(u => u.UserId == _currentUser.UserId);
            DisplayDeveloperNames();

        }
        #endregion

        #region Methods
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

        public void ViewSprintBacklog()
        {
            SprintBacklogViewerViewModel vm = new SprintBacklogViewerViewModel(_currentSprint, _currentProject, _currentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void ViewAddTasksView()
        {
            CreateUserStoryTaskViewModel vm = new CreateUserStoryTaskViewModel(_currentSprint, _currentProject, _currentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void ViewSprintManagement()
        {
            SprintManagementViewModel vm = new SprintManagementViewModel(CurrentSprint, CurrentUser, CurrentProject);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void NavigateToSprintBacklog()
        {
            SprintBacklogViewModel vm = new SprintBacklogViewModel(_currentProject, _currentSprint, _currentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void PreviousView()
        {
            ProjectSummaryViewModel vm = new ProjectSummaryViewModel(CurrentProject, CurrentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void TasksView()
        {
            SprintTaskViewsViewModel vm = new SprintTaskViewsViewModel(CurrentUser, CurrentProject, CurrentSprint);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion

    }
}
