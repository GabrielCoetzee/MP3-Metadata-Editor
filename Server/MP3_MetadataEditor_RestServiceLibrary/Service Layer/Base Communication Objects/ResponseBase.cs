using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects
{
    [DataContract]
    public abstract class ResponseBase
    {
    }

    [DataContract]
    [JsonObject]
    public class ResponseBase<T> : ResponseBase
    {
        public ResponseBase(T dataValue)
        {
            DataValue = dataValue;
            StatusCode = HttpStatusCode.OK;
        }

        [JsonProperty("DataValue")]
        [DataMember]
        public T DataValue { get; private set; }

        [JsonProperty("StatusCode")]
        [DataMember]
        public HttpStatusCode StatusCode { get; private set; }
    }
}
