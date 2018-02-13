using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MP3_MetadataEditor_RestServiceLibrary.Album_Art_Service
{
    [DataContract]
    public class MP3MetadataEditorServiceRequest
    {
        #region Constructor

        public MP3MetadataEditorServiceRequest(Body body)
        {
            Body = body;
        }

        #endregion Constructor

        #region Properties

        [JsonProperty("Body")]
        [DataMember]
        public Body Body { get; set; }

        #endregion Properties
    }

    [DataContract]
    public class Body
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
