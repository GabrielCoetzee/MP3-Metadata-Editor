using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Service.EntityLevel.Interfaces;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.External_API_Communication.LastFM_API_Communication.Objects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Communication_Objects.RequestObjects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.EntityLevel;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Proxy
{
    public class LastFmApiServiceProxy : IGetAlbumArt<LastFMAlbumPayload>
    {
        #region Properties

        private readonly IGetAlbumArt<LastFMAlbumPayload> _service;

        #endregion

        #region Constructor

        public LastFmApiServiceProxy()
        {
            _service = new LastFmApiService();
        }

        #endregion

        #region Public Methods

        public ResponseBase<LastFMAlbumPayload> GetAlbumArt(RequestBase request)
        {
            return _service.GetAlbumArt(request as LastFMServiceRequest);
        }

        #endregion

    }
}
