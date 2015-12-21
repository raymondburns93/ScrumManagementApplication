using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.SprintBacklogService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    /// <summary>
    /// Author: Andrew Baird
    /// Description: Gets the sprint backlog for a specified sprint
    /// </summary>
    public class SprintBacklogViewerViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Variables
        private SprintBacklogServiceClient _sprintBacklogService = new SprintBacklogServiceClient();

        private UserDTO _currentUser { get; set; }
        private ProjectDTO _currentProject { get; set; }

        private SprintDTO _currentSprint { get; set; }

        private List<UserStoryDTO> _sprintList;

        private ICommand _backCommand;

        #endregion

        #region Getters/Setters
        public ICommand BackCommand
        {
            get
            {
                return _backCommand ??
                (_backCommand = new RelayCommand1(param => PreviousView(), param => true));
            }
        }

        public List<UserStoryDTO> SprintList
        {
            get { return _sprintList; }
            set { _sprintList = value; }
        }

        public SprintDTO CurrentSprint
        {
            get { return _currentSprint; }
            set { _currentSprint = value; }
        }

        public string Name
        {
            get { return "SprintBacklogViewerView"; }
        }
        #endregion

        #region Constructor
        public SprintBacklogViewerViewModel(SprintDTO sprintDTO, ProjectDTO projectDTO, UserDTO userDTO)
        {
            CurrentSprint = sprintDTO;
            _currentProject = projectDTO;
            _currentUser = userDTO;
            RefreshProductBacklog();
        }
        #endregion

        #region Methods
        public void RefreshProductBacklog()
        {
            SprintList = _sprintBacklogService.GetSprintBacklogUserStories((int)_currentSprint.SprintBacklogId);
        }

        public void PreviousView()
        {
            SprintSummaryViewModel vm = new SprintSummaryViewModel(_currentSprint, _currentUser, _currentProject);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion

    }
}
