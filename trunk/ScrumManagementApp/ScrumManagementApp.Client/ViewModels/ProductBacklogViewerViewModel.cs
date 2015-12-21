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
    /// Description: Gets the product backlog for a specified project
    /// </summary>
    public class ProductBacklogViewerViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Constructor
        public ProductBacklogViewerViewModel(ProjectDTO projectDTO, UserDTO userDTO)
        {
            CurrentProject = projectDTO;
            CurrentUser = userDTO;
            RefreshProductBacklog();
        }
        #endregion

        #region Variables
        private ProductBacklogServiceClient _productBacklogService = new ProductBacklogServiceClient();

        private ProjectDTO _currentProject { get; set; }
        
        private UserDTO _currentUser { get; set; }

        private List<UserStoryDTO> _userStoryList;

        private ICommand _backCommand;

        #endregion

        #region Getters/Setters
        public ProjectDTO CurrentProject
        {
            get { return _currentProject; }
            set { _currentProject = value; }
        }

        public UserDTO CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public List<UserStoryDTO> UserStoryList
        {
            get { return _userStoryList; }
            set { _userStoryList = value; }
        }
        public string Name
        {
            get { return "ProductBacklogViewerView"; }
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

        #region Methods
        public void RefreshProductBacklog()
        {
            UserStoryList = _productBacklogService.GetProductBacklogUserStories(_currentProject.ProjectId);
        }

        public void PreviousView()
        {
            ProjectSummaryViewModel vm = new ProjectSummaryViewModel(CurrentProject, CurrentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion

    }
}
