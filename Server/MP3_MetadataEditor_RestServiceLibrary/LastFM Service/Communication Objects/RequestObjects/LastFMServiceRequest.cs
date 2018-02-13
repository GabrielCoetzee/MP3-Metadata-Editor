using System.Runtime.Serialization;

namespace MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Communication_Objects.RequestObjects
{
    [DataContract]
    public class LastFMServiceRequest
    {
        #region Constructor
        public LastFMServiceRequest()
        {
        }

        public LastFMServiceRequest(string artist, string song)
        {
            Artist = artist;
            Song = song;
        }

        #endregion Constructor

        #region Properties

        private string _artist;
        private string _song;

        public string Song
        {
            get { return _song; }
            set { _song = value; }
        }

        public string Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }

        #endregion Properties
    }
}
