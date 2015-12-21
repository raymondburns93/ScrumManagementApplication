using System;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.SprintBacklogService;
using ScrumManagementApp.Client.TaskService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    class CreateUserStoryTaskViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Constructor
        public CreateUserStoryTaskViewModel(SprintDTO sprint, ProjectDTO project, UserDTO user)
        {
            _currentSprint = sprint;
            // _currentUserStory = userStory;
            _currentProject = project;
            _currentUser = user;
            RefreshSprintBacklog();

        }
        #endregion

        #region Variables
        private SprintBacklogServiceClient _sprintBacklogService = new SprintBacklogServiceClient();

        // Sends the clicked User Story on the Sprint Backlog to the Create Task View
        private ICommand _setCurrentUserStory;

        private List<UserStoryDTO> _sprintTaskList;
        public List<UserStoryDTO> SprintTaskList
        {
            get { return _sprintTaskList; }
            set
            {
                _sprintTaskList = value;
                OnPropertyChanged("SprintTaskList");
            }
        }

        private TaskServiceClient _taskService = new TaskServiceClient();

        private ICommand _addCommand;

        private ICommand _backCommand;

        private SprintDTO _currentSprint;
        private ProjectDTO _currentProject;
        private UserStoryDTO _currentUserStory { get; set; }
        private UserDTO _currentUser { get; set; }

        private string _txtTaskName = "";
        private string _txtTaskDescription = "";
        private string _txtTaskTimeEstimate = "";
        private int _estimate;
        private string _lblErrorMessage = "";

        private string _userStoryDescription { get; set; }

        private const string _ERROR_ENTER_TASK_NAME = "Enter task name";
        private const string _ERROR_ENTER_HOURS_REMAINING = "Enter hours remaing for task";
        private const string _ERROR_HOURS_REMAINING_BAD_FORMAT = "Hours remaining is not in a valid format";
        private const string _ERROR_TIME_ESTIMATE_TOO_LONG = "The estimated time for this task is greater than 8 hours, please split into smaller tasks";
        private const string _TASK_CREATED = "Task created";
        #endregion

        #region Getters/Setters
        public string Name
        {
            get { return "Create Task"; }
        }

        public ICommand AddCommand
        {
            get
            {
                return _addCommand ??
                       (_addCommand = new RelayCommand1(param => AddTask(), param => true));
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ??
                       (_backCommand = new RelayCommand1(param => NavigateToSprintBacklog(), param => true));
            }
        }


        public string UserStoryDescription
        {
            get { return _userStoryDescription; }
            set
            {
                _userStoryDescription = value;
                OnPropertyChanged("UserStoryDescription");
            }
        }

        public string txtTaskNameProperty
        {
            get { return _txtTaskName; }
            set { _txtTaskName = value; }
        }

        public string txtTaskDescriptionProperty
        {
            get { return _txtTaskDescription; }
            set { _txtTaskDescription = value; }
        }

        public string txtTaskTimeEstimateProperty
        {
            get { return _txtTaskTimeEstimate; }
            set { _txtTaskTimeEstimate = value; }
        }

        public string lblErrorMessageProperty
        {
            get { return _lblErrorMessage; }
            set
            {
                _lblErrorMessage = value;
                OnPropertyChanged("lblErrorMessageProperty");
            }
        }

        public ICommand SetCurrentUserStory
        {
            get
            {
                return _setCurrentUserStory ??
                    (_setCurrentUserStory = new RelayCommand1(param => SetCurrentUS((UserStoryDTO)param), param => true));
            }
        }

        #endregion

        #region Methods
        private void AddTask()
        {
            if (TaskValid())
            {
                //save task
                TaskDTO taskToAdd = new TaskDTO();
                taskToAdd.TaskName = _txtTaskName;
                taskToAdd.TaskDescription = _txtTaskDescription;
                taskToAdd.HoursRemaining = TimeSpan.FromHours(_estimate);
                taskToAdd.UserStoryID = _currentUserStory.UserStoryID;
                _taskService.AddTask(taskToAdd);

                _lblErrorMessage = _TASK_CREATED;
            }
        }

        private void SetCurrentUS(UserStoryDTO selectedStory)
        {
            _currentUserStory = selectedStory;
            UserStoryDescription = selectedStory.Description;
        }

        public void RefreshSprintBacklog()
        {
            SprintTaskList = _sprintBacklogService.GetSprintBacklogUserStories((int)_currentSprint.SprintBacklogId);
        }

        public bool TaskValid()
        {
            //add validation
            bool isValid = true;

            if (_txtTaskName.Trim() == String.Empty)
            {
                _lblErrorMessage = _ERROR_ENTER_TASK_NAME;
                isValid = false;
            }
            if (isValid)
            {
                if (_txtTaskTimeEstimate.Trim() == String.Empty)
                {
                    _lblErrorMessage = _ERROR_ENTER_HOURS_REMAINING;
                    isValid = false;
                }
            }
            if (isValid)
            {
                try
                {
                    _estimate = Convert.ToInt32(_txtTaskTimeEstimate.Trim());
                }
                catch (Exception)
                {
                    _lblErrorMessage = _ERROR_HOURS_REMAINING_BAD_FORMAT;
                    isValid = false;
                }
            }
            if (isValid)
            {
                if (_estimate > 8)
                {
                    _lblErrorMessage = _ERROR_TIME_ESTIMATE_TOO_LONG;
                    isValid = false;
                }
            }
            return isValid;
        }

        private void NavigateToSprintBacklog()
        {
            SprintBacklogViewModel vm = new SprintBacklogViewModel(_currentProject, _currentSprint, _currentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion
    }
}
