using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Service.EntityLevel.Interfaces
{
    interface IGetAlbumArt<T>
    {
        ResponseBase<T> GetAlbumArt(RequestBase request);
    }
}
