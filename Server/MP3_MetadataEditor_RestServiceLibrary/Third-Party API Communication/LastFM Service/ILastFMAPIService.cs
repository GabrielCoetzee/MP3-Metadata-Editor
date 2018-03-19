using MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Communication_Objects;
using MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Communication_Objects.RequestObjects;

namespace MP3_MetadataEditor_RestServiceLibrary.LastFM_Service
{
    interface ILastFMApiService
    {
        LastFmServiceResponse GetAlbumArt(LastFMServiceRequest request);
    }
}
