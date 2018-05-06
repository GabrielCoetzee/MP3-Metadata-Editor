using MP3_MetadataEditor_Client.MVVM.Models;

namespace MP3_MetadataEditor_Client.Logic.Interfaces
{
    public interface IModifyMp3Metadata
    {
        ModelMp3 GetMP3Metadata(string path);

        void SaveMP3(ModelMp3 metadata);

        void Dispose();
    }
}
