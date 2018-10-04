using System;
using System.IO;
using System.Linq;
using System.Net;
using MP3_MetadataEditor_RestServiceLibrary.MP3MetadataEditor_Service.Helpers.Constants;
using MP3_MetadataEditor_RestServiceLibrary.MP3MetadataEditor_Service.Helpers.Converters;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.External_API_Communication.LastFM_API_Communication.Objects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Communication_Objects.RequestObjects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Proxy;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.MP3MetadataEditor_Service.Client_Communication_Objects.RequestObjects;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.MP3MetadataEditor_Service.Client_Communication_Objects.ResponseObjects;
using MP3_MetadataEditor_Server.Context_Classes;
using MP3_MetadataEditor_Server.Data_Access_Layer.Persistence;
using MP3_MetadataEditor_Server.Models;

namespace MP3_MetadataEditor_RestServiceLibrary
{
    public class Mp3MetadataEditorService : IMp3MetadataEditorService
    {
        public AddMp3Response AddMP3(AddMp3Request request)
        {
            int success = 0;

            var mp3 = new MP3()
            {
                Album = request.Body.Album,
                AlbumArt = Convert.FromBase64String(request.Body.AlbumArt), //The only reason for intermediary object, cannot send byte[] over service, has to be string
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

            using (var unitOfWork = new UnitOfWork(new MP3DbContext()))
            {
                unitOfWork.Mp3s.AddMp3(mp3);
                success = unitOfWork.Complete();
            }

            var response = new AddMp3Response((success != 0));

            return response;
        }
        public GetAlbumArtResponse GetAlbumArt(string artist, string song)
        {
            var proxy = new LastFmApiServiceProxy();

            LastFMServiceRequest lastFmServiceRequest = new LastFMServiceRequest(new LastFmRequestIntermediaryObjectBody() {Artist = artist, Song = song});

            ResponseBase<LastFMAlbumPayload> lastFmServiceResponse = proxy.GetAlbumArt(lastFmServiceRequest);

            string lastFmImageUrl = lastFmServiceResponse?.DataValue.Album.Image[2].Text;

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

            var response = new GetAlbumArtResponse(Convert.ToBase64String(imageToReturn));

            return response;
        }
    }
}
