using System;
using System.IO;
using System.Net;
using System.Text;
using MP3_MetadataEditor_Client.MP3MetadataEditorService;
using Newtonsoft.Json;

namespace MP3_MetadataEditor_Client.Service_Communication
{
    public class Mp3MetadataEditorService : IMp3MetadataEditorService
    {
        private string getAlbumArtAPIUri = @"http://localhost:8733/api/MP3MetadataEditorService/GetAlbumArt?";
        private string addMP3APIUri = @"http://localhost:8733/api/MP3MetadataEditorService/AddMP3";

        public string GetAlbumArt(string artist, string song)
        {
            HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(getAlbumArtAPIUri + $"Artist={artist}&Song={song}");
            apiRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)apiRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public MP3MetadataEditorServiceResponse AddMp3(MP3MetadataEditorServiceRequest request)
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
