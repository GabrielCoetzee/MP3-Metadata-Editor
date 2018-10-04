using System.IO;
using System.Net;
using MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Constants;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Service.EntityLevel.Interfaces;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.External_API_Communication.LastFM_API_Communication.Objects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Communication_Objects.RequestObjects;
using Newtonsoft.Json;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.EntityLevel
{
    public class LastFmApiService : IGetAlbumArt<LastFMAlbumPayload>
    {
        public ResponseBase<LastFMAlbumPayload> GetAlbumArt(RequestBase req)
        {
            var request = req as LastFMServiceRequest;

            HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(LastFMAPIConfiguration.api + LastFMAPIConfiguration.apiKey + $"&artist={request?.Body.Artist.Replace(' ', '+')}&album={request?.Body.Song.Replace(' ', '+')}" + LastFMAPIConfiguration.returnFormat);
            apiRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse res = (HttpWebResponse)apiRequest.GetResponse())
            using (Stream stream = res.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var dataValue = JsonConvert.DeserializeObject<LastFMAlbumPayload>(reader.ReadToEnd());

                var response = new ResponseBase<LastFMAlbumPayload>(dataValue);

                return response;
            }
        }
    }
}
