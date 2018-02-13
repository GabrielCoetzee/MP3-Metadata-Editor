using System.ServiceModel;
using System.ServiceProcess;

namespace MP3_MetadataEditor_WindowsService
{
    public partial class MP3MetadataEditorWindowsService : ServiceBase
    {
        #region Properties

        ServiceHost serviceHost = null;

        #endregion Properties

        public MP3MetadataEditorWindowsService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            serviceHost = new ServiceHost(typeof(MP3_MetadataEditor_RestServiceLibrary.MP3MetadataEditorService));
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
