using System;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.ProductBacklogService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    /// <summary>
    /// Author: Andrew Baird
    /// ViewModel for 'ProductBacklogView'
    /// The view is only shown to product owners of the project
    /// The constructor accepts a ProjectDTO
    /// </summary>
    public class ProductBacklogViewModel : ViewModelBase, INestedPageViewModel
    {

        #region Variables
        private ICommand _deleteTaskCommand;

        ProductBacklogServiceClient service = new ProductBacklogServiceClient();

        private UserDTO _currentUser { get; set; }

        private ProjectDTO _currentProject { get; set; }

        private string _projectName = "";

        private List<UserStoryDTO> _userStoryList;

        private RelayCommand1 _addTaskCommand;

        private string _userStoryText = "";

        private int _userStoryPriority;

        private string _errorMessage = "";
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        private ICommand _backCommand;
        #endregion

        #region Getters/Setters
        public string Name
        {
            get { return "Product Backlog View"; }
        }

        public ProjectDTO CurrentProject
        {
            get { return _currentProject; }
            set { _currentProject = value; }
        }

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
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

        public string UserStoryText
        {
            get { return _userStoryText; }
            set { _userStoryText = value; }
        }

        public int UserStoryPriority
        {
            get { return _userStoryPriority; }
            set { _userStoryPriority = value; }
        }

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ??
                (_backCommand = new RelayCommand1(param => PreviousView(), param => true));
            }
        }

        #endregion

        #region Constructor
        public ProductBacklogViewModel(ProjectDTO project, UserDTO user)
        {
            _currentProject = project;
            _currentUser = user;
            _projectName = project.ProjectName;

            RefreshProductBacklog();
        }
        #endregion

        #region Methods
        public void RefreshProductBacklog()
        {
            UserStoryList = GetProjectUserStories(_currentProject.ProjectId);
        }

        public void DeleteUserStory(UserStoryDTO us)
        {
            DeleteUserStory(_currentProject.ProjectId, us);
            ErrorMessage = "User Story Deleted!";
            RefreshProductBacklog();
        }

        // Checks if the User Story is valid before adding
        public void ValidateUserStory()
        {
            bool isValid = true;

            isValid = ValidateUserStory(ref isValid);

            if (isValid)
            {
                ErrorMessage = string.Empty;

                UserStoryDTO usDTO = new UserStoryDTO();

                usDTO.Description = UserStoryText;
                usDTO.Priority = UserStoryPriority;

                AddUserStory(_currentProject.ProjectId, usDTO);
            }
        }

        public bool ValidateUserStory(ref bool isValid)
        {
            if (String.IsNullOrWhiteSpace(UserStoryText))
            {
                isValid = false;
                ErrorMessage = "UserStory Text is empty";
            }
            if (UserStoryPriority > int.MaxValue || UserStoryPriority <= 0)
            {
                isValid = false;
                ErrorMessage = "Priority is outside of valid range (1 - int max)";
            }

            return isValid;
        }

        public void AddUserStory(int projectId, UserStoryDTO usDTO)
        {
            service.AddUserStoryToProjectBacklog(projectId, usDTO);
            ErrorMessage = "User Story Added!";
            RefreshProductBacklog();
        }

        public void EditUserStory(int projectId, UserStoryDTO usDTO)
        {
            service.EditUserStory(projectId, usDTO);
        }
        public void DeleteUserStory(int projectId, UserStoryDTO usDTO)
        {
            service.DeleteUserStory(projectId, usDTO);
        }
        public List<UserStoryDTO> GetProjectUserStories(int projectId)
        {
            return service.GetProductBacklogUserStories(projectId);
            // TODO - Output results in the datagrid
        }

        public void PreviousView()
        {
            ProjectSummaryViewModel vm = new ProjectSummaryViewModel(_currentProject, _currentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion

        #region Commands
        // Command for the Add task button
        public ICommand AddTaskCommand
        {
            get
            {
                if (_addTaskCommand == null)
                {
                    _addTaskCommand = new RelayCommand1(param => ValidateUserStory(), param => true);
                }
                return _addTaskCommand;
            }
        }

        public ICommand DeleteTaskCommand
        {
            get
            {
                return _deleteTaskCommand ??
                    (_deleteTaskCommand = new RelayCommand1(param => DeleteUserStory((UserStoryDTO)param), param => true));
            }
        }


        #endregion
    }
}
