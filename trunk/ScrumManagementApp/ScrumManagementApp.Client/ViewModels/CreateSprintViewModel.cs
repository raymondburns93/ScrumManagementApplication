using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.Interfaces;
using ScrumManagementApp.Client.SprintService;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.Client.ViewModels
{
    public class CreateSprintViewModel : ViewModelBase, INestedPageViewModel
    {

        #region Constructors
        public CreateSprintViewModel(UserDTO user, ProjectDTO project)
        {
            CurrentUser = user;
            Project = project;
        }
        #endregion

        #region Getters/Setters
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                       (_saveCommand = new RelayCommand1(param => CreateSprint(), param => true));
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

        public UserDTO CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public ProjectDTO Project
        {
            get { return _project; }
            set { _project = value; }
        }

        public string SprintName
        {
            get { return _sprintName; }
            set { _sprintName = value; }
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

        public string Name { get { return "Create Sprint"; } }

        public string SprintNameError
        {
            get { return _sprintNameError; }
            set
            {
                _sprintNameError = value;
                OnPropertyChanged("SprintNameError");
            }
        }

        public string StartDateError
        {
            get { return _startDateError; }
            set
            {
                _startDateError = value;
                OnPropertyChanged("StartDateError");
            }
        }

        public string EndDateError
        {
            get { return _endDateError; }
            set
            {
                _endDateError = value;
                OnPropertyChanged("EndDateError");
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

        public string ViewMessage
        {
            get { return _viewMessage; }
            set
            {
                _viewMessage = value;
                OnPropertyChanged("ViewMessage");
            }
        }
        #endregion

        #region Variables
        SprintServiceClient _sprintService = new SprintServiceClient();

        private ICommand _saveCommand;

        private ICommand _backCommand;

        private UserDTO _currentUser { get; set; }
        
        private ProjectDTO _project { get; set; }
        
        private string _sprintName = "";
        
        private DateTime _startDate;
        
        private DateTime? _endDate;
        
        private string _sprintNameError = "";
        
        private string _startDateError = "";
        
        private string _endDateError = "";
        
        private string _endDateString = "";
        
        private string _startDateString = "";
        
        private string _viewMessage = "";
        
        #endregion

        #region Methods
        public void CreateSprint()
        {
            if (ValidateSprintDetails())
            {
                SprintDTO sprint = new SprintDTO();
                sprint.SprintName = SprintName;
                sprint.StartDate = StartDate;
                sprint.EndDate = EndDate;
                sprint.ProjectId = Project.ProjectId;

                _sprintService.CreateSprint(sprint, CurrentUser.UserId);
                ViewMessage = "Sprint Created!";
            }
        }

        public bool ValidateSprintDetails()
        {
            bool isValid = true;

            // Sprint name is valid
            if (_sprintName == String.Empty)
            {
                SprintNameError = "Sprint Name Cannot be Empty";
                isValid = false;
            }
            else
                SprintNameError = "";

            // Start date has been set
            if (StartDateString.Equals(""))
            {
                StartDateError = "Select a start date";
                isValid = false;
            }
            else
                StartDateError = "";

            if (EndDateString.Equals(""))
            {
                EndDateError = "Select an end date";
                isValid = false;
            }
            else
                EndDateError = "";

            // Valid start and end date entered - check end date takes place after the start date
            if (!EndDateString.Equals("") && !StartDateString.Equals(""))
            {
                if (_endDate < _startDate)
                {
                    ViewMessage = "End date cannot be before start date";
                    isValid = false;
                }
            }
            else
                ViewMessage = "";

            return isValid;
        }

        public void PreviousView()
        {
            ProjectSummaryViewModel vm = new ProjectSummaryViewModel(Project, CurrentUser);
            Messenger.Default.Send<INestedPageViewModel>(vm);
        }
        #endregion

    }
}
