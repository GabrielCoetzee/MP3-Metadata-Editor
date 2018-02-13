using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization;

namespace MP3_MetadataEditor_RestServiceLibrary.Album_Art_Service
{
    [DataContract]
    public class MP3MetadataEditorServiceResponse
    {
        #region Constructor
        public MP3MetadataEditorServiceResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        #endregion Constructor

        #region Properties

        [JsonProperty("StatusCode")]
        [DataMember]
        public HttpStatusCode StatusCode { get; set; }  

        #endregion Properties
    }
}
