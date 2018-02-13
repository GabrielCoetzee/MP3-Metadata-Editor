using MP3_MetadataEditor_RestServiceLibrary.Album_Art_Service;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MP3_MetadataEditor_RestServiceLibrary
{
    [ServiceContract]
    public interface IMP3MetadataEditorService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/AddMP3")]
        MP3MetadataEditorServiceResponse AddMP3(MP3MetadataEditorServiceRequest request);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        UriTemplate = "/GetAlbumArt?Artist={artist}&Song={song}")]
        string GetAlbumArt(string artist, string song);
    }
}
