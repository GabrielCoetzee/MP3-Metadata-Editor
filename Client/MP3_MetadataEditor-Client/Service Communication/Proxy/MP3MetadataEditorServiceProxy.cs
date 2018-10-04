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

        public AddMp3Response AddMP3(AddMp3Request request)
        {
            return _service.AddMP3(request);
        }

        public GetAlbumArtResponse GetAlbumArt(string artist, string song)
        {
            return _service.GetAlbumArt(artist, song);
        }

        #endregion


    }
}
