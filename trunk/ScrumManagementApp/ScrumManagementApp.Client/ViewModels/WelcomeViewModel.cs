using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.ProjectService;
using ScrumManagementApp.Client.SprintService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    public class WelcomeViewViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Variables
        private ProjectServiceClient _projectService = new ProjectServiceClient();
        private SprintServiceClient _sprintService = new SprintServiceClient();

        private int _numProjects { get; set; }

        private int _numSprints { get; set; }

        private ICommand _changeProjectSummaryPageCommand;
        public ICommand ChangeProjectSummaryPageCommand
        {
            get
            {
                return _changeProjectSummaryPageCommand ??
                    (_changeProjectSummaryPageCommand = new RelayCommand1(ChangeProjectSummaryView));
            }
        }

        private ProjectDTO _projectDTO { get; set; }

        private UserDTO _currentUser { get; set; }
        
        private string _email = "";
        
        private string _listProjectDetails = "";
        
        private ProjectDTO[] _projectArray { get; set; }
        
        private List<SprintDTO> _sprintList { get; set; }
        
        #endregion

        #region Getters/Setters

        public int NumProjects
        {
            get { return _numProjects; }
            set
            {
                _numProjects = value;
                OnPropertyChanged("NumProjects");
            }
        }

        public int NumSprints
        {
            get { return _numSprints; }
            set
            {
                _numSprints = value;
                OnPropertyChanged("NumSprints");
            }
        }

        public UserDTO CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string ProjectDetailsProperty
        {
            get { return _listProjectDetails; }
            set { _listProjectDetails = value; }
        }

        public ProjectDTO[] ProjectArray
        {
            get { return _projectArray; }
            set
            {
                _projectArray = value;
                NumProjects = _projectArray.Length;
                OnPropertyChanged("ProjectArray");
            }
        }

        public List<SprintDTO> SprintList
        {
            get { return _sprintList; }
            set
            {
                _sprintList = value;
                NumSprints = _sprintList.Count;
                OnPropertyChanged("SprintArray");
            }
        }

        public string Name
        {
            get { return "Welcome View"; }
        }

        #endregion

        #region Constructor
        public WelcomeViewViewModel(UserDTO user)
        {
            CurrentUser = user;
            Email = user.Email;
            RefreshProjectArray();
            RefreshSprintArray();
        }
        #endregion

        #region Methods
        public void RefreshProjectArray()
        {
            ProjectArray = _projectService.GetProjectsForUser(CurrentUser.UserId);
        }

        public void RefreshSprintArray()
        {
            SprintList = _sprintService.GetSprintsForUser(CurrentUser.UserId);
        }

        public void ChangeProjectSummaryView(object button)
        {
            ProjectDTO clickedProject = button as ProjectDTO;

            _projectDTO = clickedProject;

            ProjectSummary();
        }

        public void ProjectSummary()
        {
            ProjectSummaryViewModel vm = new ProjectSummaryViewModel(_projectDTO, _currentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion


    }
}