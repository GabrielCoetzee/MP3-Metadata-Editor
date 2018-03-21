using System.Net;
using System.Runtime.Serialization;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;
using Newtonsoft.Json;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.MP3MetadataEditor_Service.Client_Communication_Objects.ResponseObjects
{
    [DataContract]
    [JsonObject]
    public class Mp3MetadataEditorServiceResponse : ResponseBase
    {
        #region Constructor
        public Mp3MetadataEditorServiceResponse(HttpStatusCode statusCode)
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
