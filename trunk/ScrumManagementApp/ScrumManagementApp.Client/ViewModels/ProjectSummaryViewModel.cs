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
using ScrumManagementApp.Common.DTOs;

/// <author>
/// James Rainey
/// </author>

/// Contribution - Andrew Baird

namespace ScrumManagementApp.Client.ViewModels
{
    /// <summary>
    /// ViewModel for the ProjectSummaryView
    /// </summary>
    public class ProjectSummaryViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Variables

        private SprintDTO _sprintDTO { get; set; }

        private ProjectServiceClient _projectService = new ProjectServiceClient();
        private SprintServiceClient _sprintService = new SprintServiceClient();
        private List<SprintDTO> _sprintList { get; set; }

        private ProjectDTO _project { get; set; }

        private string _projectName = "";

        private string _projectDescription = "";

        private DateTime _projectStartDate { get; set; }

        private DateTime? _projectEndDate { get; set; }

        private Boolean _isUserScrumMaster { get; set; }

        private Boolean _isUserProductOwner { get; set; }

        private Boolean _isUserProjectManager { get; set; }

        private UserDTO _currentUser { get; set; }

        private ICommand _showProductBacklogCommand;

        private ICommand _newSprintButtonCommand;

        private ICommand _changeSprintSummaryPageCommand;
       
        #region Project Owner

        private UserDTO _productOwner { get; set; }

        private String _productOwnerFirstName = "";

        private String _productOwnerLastName = "";

        #endregion

        #region Project Manager
        private UserDTO _projectManager { get; set; }

        private String _projectManagerFirstName = "";

        private String _projectManagerLastName = "";
        #endregion

        #region Project ScrumMaster
        private UserDTO[] _projectScrumMaster { get; set; }

        private String _projectScrumMastersNames = "";

        #endregion
        #endregion

        #region Getters/Setters
        public RelayCommand ProductBacklogButtonCommand { get; private set; }

        public List<SprintDTO> SprintList
        {
            get { return _sprintList; }
            set
            {
                _sprintList = value;
                OnPropertyChanged("SprintList");
            }
        }

        public ProjectDTO Project
        {
            get { return _project; }
            set { _project = value; }
        }

        // Show if the user is of type Product Owner
        public Visibility ShowIfProductOwner
        {
            get { return IsUserProductOwner ? Visibility.Visible : Visibility.Collapsed; }
        }

        // Show if the user is of type ScrumMaster
        public Visibility ShowIfScrumMaster
        {
            get { return IsUserScrumMaster ? Visibility.Visible : Visibility.Collapsed; }
        }

        // Show if the user is of type ScrumMaster
        public Visibility ShowIfProjectManager
        {
            get { return IsUserProjectManager ? Visibility.Visible : Visibility.Collapsed; }
        }

        public string ProjectDescription
        {
            get { return _projectDescription; }
            set { _projectDescription = value; }

        }

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }

        }

        public Boolean IsUserScrumMaster
        {
            get { return _isUserScrumMaster; }
            set { _isUserScrumMaster = value; }

        }

        public DateTime? ProjectEndDate
        {
            get { return _projectEndDate; }
            set { _projectEndDate = value; }

        }

        public DateTime ProjectStartDate
        {
            get { return _projectStartDate; }
            set { _projectStartDate = value; }

        }

        public Boolean IsUserProductOwner
        {
            get { return _isUserProductOwner; }
            set { _isUserProductOwner = value; }

        }

        public Boolean IsUserProjectManager
        {
            get { return _isUserProjectManager; }
            set { _isUserProjectManager = value; }

        }

        public UserDTO CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }


        public ICommand ShowProductBacklogCommand
        {
            get
            {
                return _showProductBacklogCommand ??
                    (_showProductBacklogCommand = new RelayCommand1(param => ViewProductBacklog(), param => true));
            }
        }

        public ICommand NewSprintButtonCommand
        {
            get
            {
                return _newSprintButtonCommand ??
                    (_newSprintButtonCommand = new RelayCommand1(param => CreateNewSprint(), param => true));
            }
        }

        public ICommand ChangeSprintSummaryPageCommand
        {
            get
            {
                return _changeSprintSummaryPageCommand ??
                    (_changeSprintSummaryPageCommand = new RelayCommand1(ChangeSprintSummaryView));
            }
        }

        public string Name
        {
            get { return "Project Summary View"; }

        }

        public RelayCommand SprintBacklogButtonClickCommand { get; private set; }

        public RelayCommand AddProductOwnerClickCommand { get; private set; }

        public RelayCommand AddScrumMasterClickCommand { get; private set; }



        public string ProductOwnerFirstName
        {
            get { return _productOwnerFirstName; }
            set { _productOwnerFirstName = value; }

        }

        public string ProductOwnerLastName
        {
            get { return _productOwnerLastName; }
            set { _productOwnerLastName = value; }

        }

        public string ProjectManagerFirstName
        {
            get { return _projectManagerFirstName; }
            set { _projectManagerFirstName = value; }

        }

        public string ProjectManagerLastName
        {
            get { return _projectManagerLastName; }
            set { _projectManagerLastName = value; }

        }

        public string ProjectScrumMasterNames
        {
            get { return _projectScrumMastersNames; }
            set { _projectScrumMastersNames = value; }

        }

        #endregion

        #region Constructor
        public ProjectSummaryViewModel(ProjectDTO project, UserDTO currentUser)
        {
            SprintBacklogButtonClickCommand = new RelayCommand(NavigateToSprintBacklog);
            ProductBacklogButtonCommand = new RelayCommand(NavigateToProductBacklog);
            AddProductOwnerClickCommand = new RelayCommand(NavigateToSelectProductOwner);
            AddScrumMasterClickCommand = new RelayCommand(NavigateToSelectScrumMaster);

            Project = project;
            _projectName = project.ProjectName;
            _projectDescription = project.ProjectDescription;
            _projectStartDate = project.StartDate;
            _projectEndDate = project.EndDate;
            _projectManager = _projectService.GetProjectManagerForProject(project.ProjectId);
            _productOwner = _projectService.GetProductOwnerForProject(project.ProjectId);
            _projectScrumMaster = _projectService.GetScrumMastersForProject(project.ProjectId);

            if (_productOwner != null)
            {
                ProductOwnerFirstName = _productOwner.Firstname;
                ProductOwnerLastName = _productOwner.Lastname;
            }

            if (_projectManager != null)
            {
                ProjectManagerFirstName = _projectManager.Firstname;
                ProjectManagerLastName = _projectManager.Lastname;
            }

            if (_projectScrumMaster != null)
            {
                StringBuilder sb = new StringBuilder();

                foreach (UserDTO scrumMaster in _projectScrumMaster)
                {
                    sb.Append(scrumMaster.Firstname + " " + scrumMaster.Lastname + " ");
                }
                _projectScrumMastersNames = sb.ToString();
            }

            IsUserProjectManager = _projectService.IsProjectManager(project, currentUser.UserId);
            IsUserProductOwner = _projectService.IsProductOwner(project, currentUser.UserId);
            IsUserScrumMaster = _projectService.IsScrumMaster(project, currentUser.UserId);

            GetAllSprintsForProject();
            CurrentUser = currentUser;
        }

        #endregion

        #region Methods
        public void GetAllSprintsForProject()
        {
            SprintList = _sprintService.GetSprintsForProject(_project.ProjectId);
        }

        public void NavigateToSprintBacklog()
        {
            // SprintBacklogViewModel vm = new SprintBacklogViewModel();
            // Messenger.Default.Send<IPageViewModel>(vm);
        }

        public void NavigateToProductBacklog()
        {
            ProductBacklogViewModel vm = new ProductBacklogViewModel(_project, _currentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        private void NavigateToSelectProductOwner()
        {
            //View to change to
            SelectProductOwnerViewModel vm = new SelectProductOwnerViewModel(_project, _currentUser);

            //Send the navigation Message
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        private void NavigateToSelectScrumMaster()
        {
            //View to change to
            SelectScrumMasterViewModel vm = new SelectScrumMasterViewModel(_project, _currentUser);

            //Send the navigation Message
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void ViewProductBacklog()
        {
            ProductBacklogViewerViewModel vm = new ProductBacklogViewerViewModel(_project, _currentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void CreateNewSprint()
        {
            CreateSprintViewModel vm = new CreateSprintViewModel(_currentUser, _project);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }

        public void ChangeSprintSummaryView(object button)
        {
            SprintDTO clickedSprint = button as SprintDTO;

            _sprintDTO = clickedSprint;

            SprintSummary();
        }

        public void SprintSummary()
        {
            SprintSummaryViewModel vm = new SprintSummaryViewModel(_sprintDTO, _currentUser, _project);
            //SprintManagementViewModel vm = new SprintManagementViewModel(_sprintDTO, _currentUser, _project);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion
    }
}
