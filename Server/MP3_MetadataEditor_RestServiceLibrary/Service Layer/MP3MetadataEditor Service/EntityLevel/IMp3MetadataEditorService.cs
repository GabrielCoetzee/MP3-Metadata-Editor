using System.ServiceModel;
using System.ServiceModel.Web;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.MP3MetadataEditor_Service.Client_Communication_Objects.RequestObjects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.MP3MetadataEditor_Service.Client_Communication_Objects.ResponseObjects;

namespace MP3_MetadataEditor_RestServiceLibrary
{
    [ServiceContract]
    public interface IMp3MetadataEditorService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/AddMP3")]
        Mp3MetadataEditorServiceResponse AddMP3(Mp3MetadataEditorServiceRequest request);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        UriTemplate = "/GetAlbumArt?Artist={artist}&Song={song}")]
        string GetAlbumArt(string artist, string song);
    }
}
