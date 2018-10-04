using System.Runtime.Serialization;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;
using Newtonsoft.Json;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.MP3MetadataEditor_Service.Client_Communication_Objects.
    RequestObjects
{
    [DataContract]
    public class AddMp3Request : RequestBase<Mp3RequestIntermediaryObjectBody>
    {
        #region Constructor

        public AddMp3Request(Mp3RequestIntermediaryObjectBody body)
        {
            Body = body;
        }

        #endregion Constructor
    }

    [DataContract]
    public class Mp3RequestIntermediaryObjectBody
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string AlbumArt { get; set; }
        [DataMember]
        public string Album { get; set; }
        [DataMember]
        public string Artist { get; set; }
        [DataMember]
        public string Genre { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public uint? TrackNumber { get; set; }
        [DataMember]
        public uint? Year { get; set; }
        [DataMember]
        public string Lyrics { get; set; }
        [DataMember]
        public string Composer { get; set; }
        [DataMember]
        public string SongTitle { get; set; }
    }
}

