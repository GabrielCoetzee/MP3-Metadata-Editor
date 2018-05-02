using System;
using System.IO;
using System.Linq;
using System.Net;
using MP3_MetadataEditor_RestServiceLibrary.MP3MetadataEditor_Service.Helpers.Constants;
using MP3_MetadataEditor_RestServiceLibrary.MP3MetadataEditor_Service.Helpers.Converters;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Communication_Objects.RequestObjects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Communication_Objects.ResponseObjects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Proxy;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.MP3MetadataEditor_Service.Client_Communication_Objects.RequestObjects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.MP3MetadataEditor_Service.Client_Communication_Objects.ResponseObjects;
using MP3_MetadataEditor_Server.Context_Classes;
using MP3_MetadataEditor_Server.Models;

namespace MP3_MetadataEditor_RestServiceLibrary
{
    public class Mp3MetadataEditorService : IMp3MetadataEditorService
    {
        public Mp3MetadataEditorServiceResponse AddMP3(Mp3MetadataEditorServiceRequest request)
        {
            int success = 0;

            var mp3 = new MP3()
            {
                Album = request.Body.Album,
                AlbumArt = Convert.FromBase64String(request.Body.AlbumArt),
                Artist = request.Body.Artist,
                Comments = request.Body.Artist,
                Composer = request.Body.Composer,
                Genre = request.Body.Genre,
                Lyrics = request.Body.Lyrics,
                SongTitle = request.Body.SongTitle,
                TrackNumber = request.Body.TrackNumber,
                Year = request.Body.Year,
                DateAdded = DateTime.Now
            };

            using (var mp3DbContext = new MP3DbContext())
            {
                if (mp3DbContext.MP3Sets.Where(x => x.SongTitle == mp3.SongTitle && x.Artist == mp3.Artist).FirstOrDefault() == null)
                {
                    mp3DbContext.MP3Sets.Add(mp3);
                    success = mp3DbContext.SaveChanges();
                }
            }

            return new Mp3MetadataEditorServiceResponse(success == 1 ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
        }
        public string GetAlbumArt(string artist, string song)
        {
            var proxy = new LastFmApiServiceProxy();

            LastFMServiceRequest lastFmServiceRequest = new LastFMServiceRequest() { Artist = artist, Song = song };
            LastFMServiceResponse lastFmServiceResponse = proxy.GetAlbumArt(lastFmServiceRequest) as LastFMServiceResponse;

            string lastFmImageUrl = lastFmServiceResponse?.Album?.Image[2].Text;

            string fileName = lastFmImageUrl?.Remove(0, lastFmImageUrl.LastIndexOf("/") + 1).TrimEnd('"', '\''); //+1 is to remove slash at the beginning
            string fullPathToAlbumArtOnDisk = fileName?.Length >= 1 ? Paths.TempAlbumArtPath + fileName : string.Empty;

            if (!File.Exists(fullPathToAlbumArtOnDisk) && !string.IsNullOrEmpty(lastFmImageUrl)) 
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(new Uri(lastFmImageUrl), fullPathToAlbumArtOnDisk);
                }
            }

            byte[] imageToReturn = fullPathToAlbumArtOnDisk != string.Empty ? Converters.ConvertToByteArray(fullPathToAlbumArtOnDisk) : new byte[1];

            return Convert.ToBase64String(imageToReturn);
        }
    }
}
