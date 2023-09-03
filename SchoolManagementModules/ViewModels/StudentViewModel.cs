using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SchoolManagementModules.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementModules.ViewModels
{
    public class StudentViewModel : BindableBase
    {
        private DataTable dataTable;
        private DataRowView _selectedRow;
        private DatabaseOperations _connection;
        private string _searchText;
        private IDialogService _dialogService;
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand DoubleClickCommand { get; private set; }
        private readonly IEventAggregator _eventAggregator;
        public ICollectionView CollectionView { get; set; }

        public StudentViewModel(IEventAggregator eventAggregator,IDialogService dialogService )
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            CloseCommand = new DelegateCommand(CloseCommandFunction, CanSubmit);
            SaveCommand = new DelegateCommand(SaveCommandFunction, CanSubmit);
            DoubleClickCommand = new DelegateCommand(SelectedItemFunction, CanSubmit);
            _eventAggregator.GetEvent<PassDatabaseObjectEvent>().Subscribe(GetDatabaseConnection, ThreadOption.UIThread);
        }

        public DataRowView SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; }
        }
        private void SelectedItemFunction()
        {
            int t = System.Convert.ToInt32(_selectedRow.Row["GRN"]);
        }

        private void SaveCommandFunction()
        {
            int k = 0;
        }

        private string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }
        private void GetDatabaseConnection(DatabaseOperations connection)
        {
            _connection = connection;
            ReadStuentInformation();

        }

        private void ReadStuentInformation()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM StudentProfile";
            cmd.Connection = _connection.SQLConnectionObject;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataTable = new DataTable("Studentprofile");
            dataAdapter.Fill(dataTable);
            RaisePropertyChanged("StudentDataSet");
        }

        public DataTable StudentDataSet
        {
            get { return dataTable; }
        }

        private void CloseCommandFunction()
        {
            _eventAggregator.GetEvent<ViewCancelEvent>().Publish();
        }
        

        public string Title => throw new NotImplementedException();

        

        private bool CanSubmit()
        {
            return true;
        }
    }
}
