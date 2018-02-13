using MP3_MetadataEditor_Client.Helpers.Converters;
using MP3_MetadataEditor_Client.Logic.Interfaces;
using MP3_MetadataEditor_Client.MVVM.Models;
using TagLib;

namespace MP3_MetadataEditor_Client.Logic.Interface_Implementations
{
    public class TaglibMP3metadataReader : IMP3MetadataReader
    {
        #region Properties

        private File _taglibMP3metadataReader;
        private ModelMP3 _metadata = new ModelMP3() { };

        public ModelMP3 Metadata
        {
            get { return _metadata; }
            set { _metadata = value; }
        }

        #endregion

        #region Interface Implementations

        public void Dispose()
        {
            _taglibMP3metadataReader.Dispose();
        }

        public ModelMP3 GetMP3Metadata(string fullPath, string displayPath)
        {
            _taglibMP3metadataReader = File.Create(fullPath);

            _metadata.AlbumArt = _taglibMP3metadataReader.Tag.Pictures.Length >= 1 ? _taglibMP3metadataReader.Tag.Pictures[0].Data.Data : null;
            _metadata.Album = _taglibMP3metadataReader.Tag.Album;
            _metadata.Artist = _taglibMP3metadataReader.Tag.FirstPerformer;
            _metadata.Genre = _taglibMP3metadataReader.Tag.FirstGenre;
            _metadata.Comments = _taglibMP3metadataReader.Tag.Comment;
            _metadata.TrackNumber = _taglibMP3metadataReader.Tag.Track;
            _metadata.Year = _taglibMP3metadataReader.Tag.Year;
            _metadata.Lyrics = _taglibMP3metadataReader.Tag.Lyrics;
            _metadata.Composer = _taglibMP3metadataReader.Tag.FirstComposer;
            _metadata.SongTitle = _taglibMP3metadataReader.Tag.Title;
            _metadata.FullMP3Path = fullPath;
            _metadata.DisplayMP3Path = displayPath;

            return Metadata;
        }

        public void SaveMP3(ModelMP3 metadata)
        {
            SetMP3Metadata(metadata);
            _taglibMP3metadataReader.Save();
        }

        private void SetMP3Metadata(ModelMP3 metadata)
        {
            _taglibMP3metadataReader.Tag.AlbumArtists = new string[] { metadata.Artist != null ? metadata.Artist : "" };
            _taglibMP3metadataReader.Tag.Composers = new string[] { metadata.Composer != null ? metadata.Composer : "" };
            _taglibMP3metadataReader.Tag.Genres = new string[] { metadata.Genre != null ? metadata.Genre : "" };
            _taglibMP3metadataReader.Tag.Performers = new string[] { metadata.Artist != null ? metadata.Artist : "" };
            _taglibMP3metadataReader.Tag.Lyrics = metadata.Lyrics;
            _taglibMP3metadataReader.Tag.Pictures = new IPicture[] { metadata.AlbumArt != null ? BinaryImageConverter.ConvertToIPicture(metadata.AlbumArt) : new Picture { } };
            _taglibMP3metadataReader.Tag.Album = metadata.Album;
            _taglibMP3metadataReader.Tag.Comment = metadata.Comments;
            _taglibMP3metadataReader.Tag.Title = metadata.SongTitle;
            _taglibMP3metadataReader.Tag.Track = (uint)metadata.TrackNumber;
            _taglibMP3metadataReader.Tag.Year = (uint)metadata.Year;
        }

        #endregion
    }
}
