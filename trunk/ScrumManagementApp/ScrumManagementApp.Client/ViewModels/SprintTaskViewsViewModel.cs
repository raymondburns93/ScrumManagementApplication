using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.SprintBacklogService;
using ScrumManagementApp.Common.DTOs;
using System.Collections.Generic;
using System.Windows.Input;

namespace ScrumManagementApp.Client.ViewModels
{
    public class SprintTaskViewsViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Variables
        private SprintBacklogServiceClient _sprintBacklogService = new SprintBacklogServiceClient();

        private UserDTO _currentUser { get; set; }

        private SprintDTO _currentSprint { get; set; }

        private ProjectDTO _currentProject { get; set; }

        private ICommand _backCommand;

        private List<UserStoryDTO> _sprintTaskList;

        #endregion

        #region Getters/Setters
        public string Name
        {
            get { return "SprintTaskViewsViewModel"; }
        }

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ??
                (_backCommand = new RelayCommand1(param => PreviousView(), param => true));
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
        #endregion

        #region Constructor
        public SprintTaskViewsViewModel(UserDTO user, ProjectDTO project, SprintDTO sprint)
        {
            _currentUser = user;
            _currentProject = project;
            _currentSprint = sprint;

            RefreshSprintBacklog();
        }
        #endregion

        #region Methods
        public void RefreshSprintBacklog()
        {
            SprintTaskList = _sprintBacklogService.GetSprintBacklogUserStories((int)_currentSprint.SprintBacklogId);
        }

        public void PreviousView()
        {
            SprintSummaryViewModel vm = new SprintSummaryViewModel(_currentSprint, _currentUser, _currentProject);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion

    }
}
