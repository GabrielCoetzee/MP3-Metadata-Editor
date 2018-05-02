using System.Runtime.Serialization;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Communication_Objects.RequestObjects
{
    public class LastFMServiceRequest : RequestBase
    {
        #region Properties

        public string Song { get; set; }

        public string Artist { get; set; }

        #endregion Properties

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

    }
}
