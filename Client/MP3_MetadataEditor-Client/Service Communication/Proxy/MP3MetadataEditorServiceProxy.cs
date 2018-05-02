using MP3_MetadataEditor_Client.MP3MetadataEditorService;

namespace MP3_MetadataEditor_Client.Service_Communication
{
    public class Mp3MetadataEditorServiceProxy : IMp3MetadataEditorService
    {
        #region Properties

        private readonly IMp3MetadataEditorService _service;

        #endregion

        #region Constructor

        public Mp3MetadataEditorServiceProxy()
        {
            _service = new Mp3MetadataEditorService();
        }

        #endregion

        #region Public Methods

        public string GetAlbumArt(string artist, string song)
        {
            return _service.GetAlbumArt(artist, song);
        }

        public Mp3MetadataEditorServiceResponse AddMp3(Mp3MetadataEditorServiceRequest request)
        {
            return _service.AddMp3(request);
        }

        #endregion


    }
}
