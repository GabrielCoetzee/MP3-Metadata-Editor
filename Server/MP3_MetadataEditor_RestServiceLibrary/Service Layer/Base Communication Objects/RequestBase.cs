using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects
{
    [DataContract]
    public abstract class RequestBase
    {
    }

    [DataContract]
    public abstract class RequestBase<T> : RequestBase
    {
        [JsonProperty("Body")]
        [DataMember]
        public T Body { get; set; }
    }
}
