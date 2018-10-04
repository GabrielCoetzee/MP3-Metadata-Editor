using System.Net;
using System.Runtime.Serialization;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;
using Newtonsoft.Json;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.MP3MetadataEditor_Service.Client_Communication_Objects.ResponseObjects
{
    [DataContract]
    [JsonObject]
    public class AddMp3Response : ResponseBase<bool>
    {
        public AddMp3Response(bool dataValue)
            :base(dataValue)
        {
        }
    }
}
