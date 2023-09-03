using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SchoolManagementModules.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace SchoolManagementModules.ViewModels
{
    public class ExpensesViewModel : BindableBase
    {
        private DatabaseOperations _connection;
        public DelegateCommand Cancel { get; private set; }
        public DelegateCommand Save { get; private set; }
        public string Title => throw new NotImplementedException();
        private readonly IEventAggregator _eventAggregator;

        public ExpensesViewModel(IEventAggregator eventAggregator)
        {
            Cancel = new DelegateCommand(CancelFunction, CanSubmit);
            Save = new DelegateCommand(SaveFunction, CanSubmit);
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PassDatabaseObjectEvent>().Subscribe(GetDatabaseConnection,ThreadOption.UIThread);
        }

        private void GetDatabaseConnection(DatabaseOperations connection)
        {
            _connection = connection;
        }

        private void CancelFunction()
        {
            _eventAggregator.GetEvent<ViewCancelEvent>().Publish();
        }
        
        private bool CanSubmit()
        {
            return true;
        }

        
        private void SaveFunction()
        {
            throw new NotImplementedException();
        }
    }
}
