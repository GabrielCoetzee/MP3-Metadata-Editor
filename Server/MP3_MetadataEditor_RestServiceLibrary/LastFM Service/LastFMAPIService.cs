using MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Communication_Objects;
using MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Communication_Objects.RequestObjects;
using MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Constants;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace MP3_MetadataEditor_RestServiceLibrary.LastFM_Service
{
    public class LastFMAPIService : ILastFMApiService
    {
        public LastFmServiceResponse GetAlbumArt(LastFMServiceRequest request)
        {
            HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(LastFMAPIConfiguration.api + LastFMAPIConfiguration.apiKey + string.Format("&artist={0}&album={1}", request.Artist.Replace(' ', '+'), request.Song.Replace(' ', '+')) + LastFMAPIConfiguration.returnFormat);
            apiRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)apiRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<LastFmServiceResponse>(reader.ReadToEnd());
            }
        }
    }
}
