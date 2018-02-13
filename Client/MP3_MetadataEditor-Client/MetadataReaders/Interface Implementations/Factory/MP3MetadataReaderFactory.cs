
using MP3_MetadataEditor_Client.Logic.Interfaces;

namespace MP3_MetadataEditor_Client.Logic.Interface_Implementations.Factory
{
    public class MP3metadataReaderFactory
    {
        #region Constructor

        private MP3metadataReaderFactory()
        {

        }

        #endregion Constructor

        #region Properties

        private enum MP3metadataReaders { TAGLIB };
        private int selectedMP3metadataReader = (int)MP3metadataReaders.TAGLIB;

        private IMP3MetadataReader _MP3metadataReaderToReturn;

        private static MP3metadataReaderFactory _instance;
        public static MP3metadataReaderFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MP3metadataReaderFactory();
                }
                return _instance;
            }
        }

        #endregion

        #region Factory

        public IMP3MetadataReader GetSelectedMP3metadataReader()
        {
            if (_MP3metadataReaderToReturn == null)
            {
                switch (selectedMP3metadataReader)
                {
                    case (int)MP3metadataReaders.TAGLIB:
                        _MP3metadataReaderToReturn = new TaglibMP3metadataReader();
                        break;
                }
            }

            return _MP3metadataReaderToReturn;
        }

        #endregion
    }
}
