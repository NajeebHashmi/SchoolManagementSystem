using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SchoolManagementModules.Classes;
using SchoolManagementSystem.Classes;
using System;
using System.Data.SqlClient;

namespace SchoolManagementSystem.ViewModels
{
    public enum DisplayedControlType
    {
        StudentControl      =   1,
        AdmissionsControl   =   2,
        ExpensesControl     =   3,
        StaffControl        =   4,
        SettingsControl     =   5,
        ViewStaffControl    =   6
    }
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private readonly IRegionManager _regionManager;
        private DatabaseOperations _connection;
        private readonly IEventAggregator _eventAggregator;

        //DatabaseOperations _connection;
        private IDialogService _dialogService;
        public DelegateCommand<string> ShowStudents { get; private set; }
        public DelegateCommand<string> ShowExpenses { get; private set; }
        public DelegateCommand<string> ShowStaff { get; private set; }
        public DelegateCommand<string> ShowSettings { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }


        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

       

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ViewCancelEvent>().Subscribe(RestoreWindow);
            _regionManager = regionManager;
            _connection = new DatabaseOperations("", "", "SchoolAdministration", true);
            _connection.Connect();
            _dialogService = dialogService;
            ShowStudents = new DelegateCommand<string>(PerformNavigation, CanSubmit);
            ShowExpenses = new DelegateCommand<string>(PerformNavigation, CanSubmit);
            ShowStaff = new DelegateCommand<string>(PerformNavigation, CanSubmit);
            ShowSettings = new DelegateCommand<string>(PerformNavigation, CanSubmit);
            

            //SqlDataReader reader = _connection.ExecuteQuery("Select * from StudentProfile");
        }

        private void RestoreWindow()
        {
            PerformNavigation("StudentView");
        }

        private void PerformNavigation(string name)
        {
            _regionManager.RequestNavigate("MainContentRegion", name);
            _eventAggregator.GetEvent<SchoolManagementModules.Classes.PassDatabaseObjectEvent>().Publish(_connection);

        }
        private bool CanSubmit(string name)
        {
            return true;
        }

        
    }
}
