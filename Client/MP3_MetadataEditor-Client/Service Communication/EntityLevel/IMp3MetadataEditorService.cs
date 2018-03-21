using MP3_MetadataEditor_Client.MP3MetadataEditorService;

namespace MP3_MetadataEditor_Client.Service_Communication
{
    interface IMp3MetadataEditorService
    {
        string GetAlbumArt(string artist, string song);
        MP3MetadataEditorServiceResponse AddMp3(MP3MetadataEditorServiceRequest request);
    }
}
