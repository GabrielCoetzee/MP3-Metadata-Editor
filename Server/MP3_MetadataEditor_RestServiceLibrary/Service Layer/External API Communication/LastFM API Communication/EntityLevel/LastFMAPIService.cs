using System.IO;
using System.Net;
using MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Constants;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Service.EntityLevel.Interfaces;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Communication_Objects.RequestObjects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Communication_Objects.ResponseObjects;
using Newtonsoft.Json;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.EntityLevel
{
    public class LastFmApiService : ICommunicateWithExternalApi
    {
        public ResponseBase GetAlbumArt(RequestBase req)
        {
            var request = req as LastFMServiceRequest;

            HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(LastFMAPIConfiguration.api + LastFMAPIConfiguration.apiKey + $"&artist={request?.Artist.Replace(' ', '+')}&album={request?.Song.Replace(' ', '+')}" + LastFMAPIConfiguration.returnFormat);
            apiRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)apiRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var res = JsonConvert.DeserializeObject<LastFMServiceResponse>(reader.ReadToEnd()) as LastFMServiceResponse;

                return res;
            }
        }
    }
}
