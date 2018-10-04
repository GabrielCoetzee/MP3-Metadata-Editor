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
        private readonly string getAlbumArtAPIUri = @"http://localhost:8733/api/MP3MetadataEditorService/GetAlbumArt?";
        private readonly string addMP3APIUri = @"http://localhost:8733/api/MP3MetadataEditorService/AddMP3";

        public AddMp3Response AddMP3(AddMp3Request request)
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
                return JsonConvert.DeserializeObject<AddMp3Response>(reader.ReadToEnd());
            }
        }

        public GetAlbumArtResponse GetAlbumArt(string artist, string song)
        {
            HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(getAlbumArtAPIUri + $"Artist={artist}&Song={song}");
            apiRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)apiRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<GetAlbumArtResponse>(reader.ReadToEnd());
            }
        }
    }
}
