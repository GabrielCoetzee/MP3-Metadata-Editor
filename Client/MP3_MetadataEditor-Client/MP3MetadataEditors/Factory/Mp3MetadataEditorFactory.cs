using MP3_MetadataEditor_Client.Logic.Interfaces;
using MP3_MetadataEditor_Client.Logic.Interface_Implementations;

namespace MP3_MetadataEditor_Client.MetadataReaders.Interface_Implementations.Factory
{
    public class Mp3MetadataEditorFactory
    {
        #region Constructor

        private Mp3MetadataEditorFactory()
        {

        }

        #endregion Constructor

        #region Properties

        private IModifyMp3Metadata _mp3MetadataEditor;

        private static Mp3MetadataEditorFactory _instance;
        public static Mp3MetadataEditorFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Mp3MetadataEditorFactory();
                }
                return _instance;
            }
        }

        #endregion

        #region Factory

        public IModifyMp3Metadata GetMp3MetadataEditor(MP3MetadataReaderTypes.Mp3MetadataReaders selectedMp3MetadataEditor)
        {
            if (_mp3MetadataEditor == null)
            {
                switch (selectedMp3MetadataEditor)
                {
                    case MP3MetadataReaderTypes.Mp3MetadataReaders.Taglib:
                        _mp3MetadataEditor = new TaglibMp3MetadataEditorWrapper();
                        break;
                }
            }

            return _mp3MetadataEditor;
        }

        #endregion
    }
}
