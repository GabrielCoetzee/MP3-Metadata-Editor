using System.ServiceModel;
using System.ServiceProcess;
using MP3_MetadataEditor_RestServiceLibrary;

namespace MP3_MetadataEditor_WindowsService
{
    public partial class Mp3MetadataEditorWindowsService : ServiceBase
    {
        #region Properties

        ServiceHost serviceHost = null;

        #endregion Properties

        public Mp3MetadataEditorWindowsService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            serviceHost = new ServiceHost(typeof(Mp3MetadataEditorService));
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
