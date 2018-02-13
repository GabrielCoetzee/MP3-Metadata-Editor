using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Communication_Objects
{
    [DataContract]
    public class LastFmServiceResponse
    {
        public Album album { get; set; }
    }

    public class Album
    {
        public string name { get; set; }
        public string artist { get; set; }
        public string mbid { get; set; }
        public string url { get; set; }
        public Image[] image { get; set; }
        public string listeners { get; set; }
        public string playcount { get; set; }
        public Tracks tracks { get; set; }
        public Tags tags { get; set; }
    }

    public class Tracks
    {
        public Track[] track { get; set; }
    }

    public class Track
    {
        public string name { get; set; }
        public string url { get; set; }
        public string duration { get; set; }
        public Attr attr { get; set; }
        public Streamable streamable { get; set; }
        public Artist artist { get; set; }
    }

    public class Attr
    {
        public string rank { get; set; }
    }

    public class Streamable
    {
        public string text { get; set; }
        public string fulltrack { get; set; }
    }

    public class Artist
    {
        public string name { get; set; }
        public string mbid { get; set; }
        public string url { get; set; }
    }

    public class Tags
    {
        public Tag[] tag { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Image
    {
        [JsonProperty("#text")]
        public string text { get; set; }
        public string size { get; set; }
    }
}
