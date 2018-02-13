using MP3_MetadataEditor_Client.MP3MetadataEditorService;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Linq;

namespace MP3_MetadataEditor_Client.Service_Communication
{
    public class MP3MetadataEditorServiceProxy : IMP3MetadataEditorService
    {
        private string getAlbumArtAPIUri = @"http://localhost:8733/api/MP3MetadataEditorService/GetAlbumArt?";
        private string addMP3APIUri = @"http://localhost:8733/api/MP3MetadataEditorService/AddMP3";

        public string GetAlbumArt(string artist, string song)
        {
            HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(getAlbumArtAPIUri + string.Format("Artist={0}&Song={1}", artist, song));
            apiRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)apiRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public MP3MetadataEditorServiceResponse AddMP3(MP3MetadataEditorServiceRequest request)
        {
            string requestData = JsonConvert.SerializeObject(request);
            byte[] requestDataBytes = Encoding.UTF8.GetBytes(requestData); 

            HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(addMP3APIUri);
            apiRequest.Method = "POST";
            apiRequest.ContentType = "application/json; charset=UTF-8";
            apiRequest.ContentLength = requestDataBytes.Length;

            using (Stream dataStream = apiRequest.GetRequestStream())
            {
                dataStream.Write(requestDataBytes, 0, requestDataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)apiRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<MP3MetadataEditorServiceResponse>(reader.ReadToEnd());
            }
        }
    }
}
