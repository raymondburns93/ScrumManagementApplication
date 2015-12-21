using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.ProductBacklogService;
using ScrumManagementApp.Client.SprintBacklogService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    /// <summary>
    /// Author: Andrew Baird
    /// </summary>
    public class SprintBacklogViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Variables
        // Adds a User Story from the Product Backlog to the Sprint Backlog
        private ICommand _addToSprintBacklogCommand;

        // Sends the clicked User Story on the Sprint Backlog to the Create Task View
        private ICommand _addTaskToUserStory;

        private ICommand _backCommand;

        private ProductBacklogServiceClient _productBacklogService = new ProductBacklogServiceClient();
        private SprintBacklogServiceClient _sprintBacklogService = new SprintBacklogServiceClient();

        private ProjectDTO _currentProject { get; set; }

        private UserDTO _currentUser { get; set; }

        private SprintDTO _currentSprint { get; set; }

        private List<UserStoryDTO> _userStoryList;

        private List<UserStoryDTO> _sprintTaskList;

        private string _errorMessage = "";

        #endregion

        #region Getters/Setters
        public string Name
        {
            get { return "Sprint Backlog View"; }
        }

        public ICommand AddToSprintBacklogCommand
        {
            get
            {
                return _addToSprintBacklogCommand ??
                    (_addToSprintBacklogCommand = new RelayCommand1(param => AddUserStory((UserStoryDTO)param), param => true));
            }
        }

        public ICommand AddTaskToUserStory
        {
            get
            {
                return _addTaskToUserStory ??
                    (_addTaskToUserStory = new RelayCommand1(param => AddTaskToSelectedUserStory((UserStoryDTO)param), param => true));
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

        public List<UserStoryDTO> UserStoryList
        {
            get { return _userStoryList; }
            set
            {
                _userStoryList = value;
                OnPropertyChanged("UserStoryList");
            }
        }

        public List<UserStoryDTO> SprintTaskList
        {
            get { return _sprintTaskList; }
            set
            {
                _sprintTaskList = value;
                OnPropertyChanged("SprintTaskList");
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
        #endregion

        #region Constructor
        public SprintBacklogViewModel(ProjectDTO project, SprintDTO sprint, UserDTO user)
        {
            _currentProject = project;
            _currentSprint = sprint;
            _currentUser = user;

            RefreshProductBacklog();
            RefreshSprintBacklog();
        }
        #endregion

        #region Methods

        public void AddUserStory(UserStoryDTO us)
        {
            _sprintBacklogService.AddUserStoryToSprintBacklog((int)CurrentSprint.SprintBacklogId, us);

            ErrorMessage = "User story added to sprint backlog!";
            RefreshSprintBacklog();
        }

        public void RefreshProductBacklog()
        {
            UserStoryList = _productBacklogService.GetProductBacklogUserStories(_currentProject.ProjectId);
        }

        public void RefreshSprintBacklog()
        {
            SprintTaskList = _sprintBacklogService.GetSprintBacklogUserStories((int)_currentSprint.SprintBacklogId);
        }

        private void AddTaskToSelectedUserStory(UserStoryDTO selectedStory)
        {
            //CreateUserStoryTaskViewModel vm = new CreateUserStoryTaskViewModel(selectedStory, _currentSprint, _currentProject, _currentUser);
            //Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void PreviousView()
        {
            SprintSummaryViewModel vm = new SprintSummaryViewModel(_currentSprint, _currentUser, _currentProject);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion

    }
}
