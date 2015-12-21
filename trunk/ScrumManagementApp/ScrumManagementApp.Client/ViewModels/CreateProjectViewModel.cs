using System;
using System.Windows.Input;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.ProjectService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    public class CreateProjectViewModel : ViewModelBase, INestedPageViewModel
    {
        #region Constructor
        public CreateProjectViewModel(UserDTO currentUser)
        {
            CurrentUser = currentUser;
        }
        #endregion

        #region Getters/Setters
        public UserDTO CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }

        public string ProjectDescription
        {
            get { return _projectDescription; }
            set { _projectDescription = value; }
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

        public string EndDateString
        {
            get { return _endDateString; }
            set
            {
                _endDateString = value;
                EndDate = Convert.ToDateTime(value);
            }
        }

        public string StartDateString
        {
            get { return _startDateString; }
            set
            {
                _startDateString = value;
                StartDate = Convert.ToDateTime(value);
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                       (_saveCommand = new RelayCommand1(param => SaveProject(), param => true));
            }
        }

        public string Name
        {
            get { return "Create Project View"; }
        }
        #endregion

        #region Variables
        private RelayCommand1 _saveCommand;

        private UserDTO _currentUser { get; set; }
        
        private string _projectName = "";

        private string _projectDescription = "";
        
        private string _errorMessage = "";
        
        private string _endDateString = "";
        
        private string _startDateString = "";
        
        private DateTime _startDate;

        private DateTime? _endDate;
        
        private ProjectServiceClient _projectService = new ProjectServiceClient();

        private const string _ERROR_ENTER_PROJECT_NAME = "Enter project name";
        private const string _ERROR_ENTER_PROJECT_DESCRIPTION = "Enter project description";
        private const string _ERROR_ENTER_START_DATE = "Enter project start date";
        private const string _ERROR_START_DATE_AFTER_END = "Start date cannot be after the end date";
        private const string _ERROR_PROJECT_NAME_ALREADY_USED = "This project name has already been used, please enter another";
        private const string _COULDNT_SAVE_PROJECT = "An unexpected error occured, we couldn't save this project";
        private const string _PROJECT_CREATED = "Project Created";

        #endregion

        #region Methods
        private void SaveProject()
        {
            try
            {
                if (DetailsValid())
                {
                    ProjectDTO toSave = new ProjectDTO();
                    toSave.ProjectName = _projectName;
                    toSave.ProjectDescription = _projectDescription;
                    toSave.EndDate = _endDate;
                    toSave.StartDate = _startDate;

                    //save project, save currentUser as the project manager
                    _projectService.CreateProject(toSave, CurrentUser.UserId);

                    ErrorMessage = _PROJECT_CREATED;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = _COULDNT_SAVE_PROJECT + " - " + ex.Message;
            }

        }

        public bool DetailsValid()
        {
            bool isValid = true;

            // Project name has been entered
            if (_projectName == String.Empty)
            {
                ErrorMessage = _ERROR_ENTER_PROJECT_NAME;
                isValid = false;
            }

            // Project Description has been entered
            if (_projectDescription == String.Empty)
            {
                ErrorMessage = _ERROR_ENTER_PROJECT_DESCRIPTION;
                isValid = false;
            }

            // Start date has been set
            if(StartDateString.Equals(""))
            {
                ErrorMessage = _ERROR_ENTER_START_DATE;
                isValid = false;
            }

            // End date has been set and is after the start date
            if (_endDate != null) {
                if (_endDate < _startDate)
                {
                    ErrorMessage = _ERROR_START_DATE_AFTER_END;
                    isValid = false;
                }
            }

            if (_projectService.GetProjectByName(_projectName) != null)
            {
                ErrorMessage = _ERROR_PROJECT_NAME_ALREADY_USED;
                isValid = false;
            }

            return isValid;
        }
        #endregion

    }
}
