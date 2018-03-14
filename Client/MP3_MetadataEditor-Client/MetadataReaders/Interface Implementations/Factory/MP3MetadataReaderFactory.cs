using MP3_MetadataEditor_Client.Logic.Interfaces;
using MP3_MetadataEditor_Client.Logic.Interface_Implementations;

namespace MP3_MetadataEditor_Client.MetadataReaders.Interface_Implementations.Factory
{
    public class Mp3MetadataReaderFactory
    {
        #region Constructor

        private Mp3MetadataReaderFactory()
        {

        }

        #endregion Constructor

        #region Properties

        private IMP3MetadataReader _mp3MetadataReaderToReturn;

        private static Mp3MetadataReaderFactory _instance;
        public static Mp3MetadataReaderFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Mp3MetadataReaderFactory();
                }
                return _instance;
            }
        }

        #endregion

        #region Factory

        public IMP3MetadataReader GetMp3MetadataReader(int selectedMp3MetadataReader)
        {
            if (_mp3MetadataReaderToReturn == null)
            {
                switch (selectedMp3MetadataReader)
                {
                    case (int)MP3MetadataReaderTypes.Mp3MetadataReaders.Taglib:
                        _mp3MetadataReaderToReturn = new TaglibMp3MetadataReader();
                        break;
                }
            }

            return _mp3MetadataReaderToReturn;
        }

        #endregion
    }
}
