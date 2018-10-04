using System.Runtime.Serialization;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;
using Newtonsoft.Json;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.MP3MetadataEditor_Service.Client_Communication_Objects.ResponseObjects
{
    [DataContract]
    [JsonObject]
    public class GetAlbumArtResponse : ResponseBase<string>
    {
        public GetAlbumArtResponse(string dataValue) 
            : base(dataValue)
        {
        }
    }
}
