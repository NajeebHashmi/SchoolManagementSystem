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
    public class ViewStaffViewModel : BindableBase
    {
        private DatabaseOperations _connection;
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        private readonly IEventAggregator _eventAggregator;
        public ViewStaffViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CloseCommand = new DelegateCommand(CloseCommandFunction, CanSubmit);
            _eventAggregator.GetEvent<PassDatabaseObjectEvent>().Subscribe(GetDatabaseConnection, ThreadOption.UIThread);
        }

        private void GetDatabaseConnection(DatabaseOperations connection)
        {
            _connection = connection;
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
