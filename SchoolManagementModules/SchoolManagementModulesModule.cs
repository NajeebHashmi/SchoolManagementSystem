using SchoolManagementModules.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace SchoolManagementModules
{
    public class SchoolManagementModulesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Expenses>();
            containerRegistry.RegisterForNavigation<Staff>();
            containerRegistry.RegisterForNavigation<StudentView>();
            
        }
    }
}