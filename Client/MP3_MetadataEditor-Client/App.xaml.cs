using MP3_MetadataEditor_Client.Helpers.Constants;
using System.IO;
using System.Windows;

namespace MP3_MetadataEditor_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (!Directory.Exists(Paths.TempAlbumArtPath))
                Directory.CreateDirectory(Paths.TempAlbumArtPath);

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                Directory.Delete(Paths.TempAlbumArtPath, true);
            }
            catch (IOException)
            {

            }

            base.OnExit(e);
        }
    }
}
