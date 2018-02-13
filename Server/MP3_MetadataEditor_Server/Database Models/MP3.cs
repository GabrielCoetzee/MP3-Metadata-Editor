using System;
using System.Runtime.Serialization;

namespace MP3_MetadataEditor_Server.Models
{
    [DataContract]
    public class MP3
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public byte[] AlbumArt { get; set; }
        [DataMember]
        public string Album { get; set; }
        [DataMember]
        public string Artist { get; set; }
        [DataMember]
        public string  Genre { get; set; }
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
        [DataMember]
        public DateTime DateAdded { get; set; }
    }
}
