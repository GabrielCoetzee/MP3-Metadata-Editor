using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Constants
{
    public static class LastFMAPIConfiguration
    {
        public const string api = "http://ws.audioscrobbler.com/2.0/?method=album.getinfo&";
        public const string apiKey = "api_key=dc20ce473ae5996906ccbf810eb53dfc";
        public const string returnFormat = "&format=json";
    }
}
