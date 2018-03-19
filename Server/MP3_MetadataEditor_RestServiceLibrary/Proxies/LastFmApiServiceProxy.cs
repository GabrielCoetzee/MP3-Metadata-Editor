using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Communication_Objects;
using MP3_MetadataEditor_RestServiceLibrary.LastFM_Service.Communication_Objects.RequestObjects;
using MP3_MetadataEditor_RestServiceLibrary.LastFM_Service;

namespace MP3_MetadataEditor_RestServiceLibrary.Proxies
{
    public class LastFmApiServiceProxy
    {
        readonly LastFMAPIService lastFmApiService = new LastFMAPIService();

        public LastFmServiceResponse GetAlbumArt(LastFMServiceRequest request)
        {
            return lastFmApiService.GetAlbumArt(request);
        }
    }
}
