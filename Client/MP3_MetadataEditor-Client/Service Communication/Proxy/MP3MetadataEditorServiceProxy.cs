using MP3_MetadataEditor_Client.MP3MetadataEditorService;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Linq;

namespace MP3_MetadataEditor_Client.Service_Communication
{
    public class Mp3MetadataEditorServiceProxy : IMp3MetadataEditorService
    {
        public string GetAlbumArt(string artist, string song)
        {
            IMp3MetadataEditorService service = new Mp3MetadataEditorService();

            return service.GetAlbumArt(artist, song);
        }

        public MP3MetadataEditorServiceResponse AddMp3(MP3MetadataEditorServiceRequest request)
        {
            IMp3MetadataEditorService service = new Mp3MetadataEditorService();

            return service.AddMp3(request);
        }
    }
}
