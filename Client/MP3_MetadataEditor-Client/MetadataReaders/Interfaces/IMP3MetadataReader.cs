using MP3_MetadataEditor_Client.MVVM.Models;

namespace MP3_MetadataEditor_Client.Logic.Interfaces
{
    public interface IMP3MetadataReader
    {
        ModelMP3 GetMP3Metadata(string path);

        void SaveMP3(ModelMP3 metadata);

        void Dispose();
    }
}
