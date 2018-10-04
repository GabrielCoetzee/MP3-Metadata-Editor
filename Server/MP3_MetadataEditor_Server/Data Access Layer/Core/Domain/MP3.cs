using System;
using System.Runtime.Serialization;

namespace MP3_MetadataEditor_Server.Models
{
    public class MP3
    {
        public int Id { get; set; }
        public byte[] AlbumArt { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string  Genre { get; set; }
        public string Comments { get; set; }
        public uint? TrackNumber { get; set; }
        public uint? Year { get; set; }
        public string Lyrics { get; set; }
        public string Composer { get; set; }
        public string SongTitle { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
