using System.ComponentModel;
using System.ServiceProcess;

namespace MP3_MetadataEditor_WindowsService
{
    [RunInstaller(true)]
    public partial class WindowsService_Installer : System.Configuration.Install.Installer
    {
        #region Properties

        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        #endregion Properties

        public WindowsService_Installer()
        {
            InitializeComponent();

            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;

            service = new ServiceInstaller();
            service.ServiceName = "MP3 Metadata Editor Service";
            service.Description = "WCF Rest API Hosting on Windows Service";
            service.DelayedAutoStart = true;

            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
