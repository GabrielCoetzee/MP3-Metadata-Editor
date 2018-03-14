using MP3_MetadataEditor_Client.Helpers.Converters;
using MP3_MetadataEditor_Client.Logic.Interfaces;
using MP3_MetadataEditor_Client.MVVM.Models;
using TagLib;

namespace MP3_MetadataEditor_Client.Logic.Interface_Implementations
{
    public class TaglibMp3MetadataReader : IMP3MetadataReader
    {
        #region Properties

        private File _taglibMp3MetadataReader;
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
            _taglibMp3MetadataReader.Dispose();
        }

        public ModelMP3 GetMP3Metadata(string fullPath, string displayPath)
        {
            _taglibMp3MetadataReader = File.Create(fullPath);

            _metadata.AlbumArt = _taglibMp3MetadataReader.Tag.Pictures.Length >= 1 ? _taglibMp3MetadataReader.Tag.Pictures[0].Data.Data : null;
            _metadata.Album = _taglibMp3MetadataReader.Tag.Album;
            _metadata.Artist = _taglibMp3MetadataReader.Tag.FirstPerformer;
            _metadata.Genre = _taglibMp3MetadataReader.Tag.FirstGenre;
            _metadata.Comments = _taglibMp3MetadataReader.Tag.Comment;
            _metadata.TrackNumber = _taglibMp3MetadataReader.Tag.Track;
            _metadata.Year = _taglibMp3MetadataReader.Tag.Year;
            _metadata.Lyrics = _taglibMp3MetadataReader.Tag.Lyrics;
            _metadata.Composer = _taglibMp3MetadataReader.Tag.FirstComposer;
            _metadata.SongTitle = _taglibMp3MetadataReader.Tag.Title;
            _metadata.FullMP3Path = fullPath;
            _metadata.DisplayMP3Path = displayPath;

            return Metadata;
        }

        public void SaveMP3(ModelMP3 metadata)
        {
            SetMP3Metadata(metadata);
            _taglibMp3MetadataReader.Save();
        }

        private void SetMP3Metadata(ModelMP3 metadata)
        {
            _taglibMp3MetadataReader.Tag.AlbumArtists = new string[] { metadata.Artist != null ? metadata.Artist : "" };
            _taglibMp3MetadataReader.Tag.Composers = new string[] { metadata.Composer != null ? metadata.Composer : "" };
            _taglibMp3MetadataReader.Tag.Genres = new string[] { metadata.Genre != null ? metadata.Genre : "" };
            _taglibMp3MetadataReader.Tag.Performers = new string[] { metadata.Artist != null ? metadata.Artist : "" };
            _taglibMp3MetadataReader.Tag.Lyrics = metadata.Lyrics;
            _taglibMp3MetadataReader.Tag.Pictures = new IPicture[] { metadata.AlbumArt != null ? BinaryImageConverter.ConvertToIPicture(metadata.AlbumArt) : new Picture { } };
            _taglibMp3MetadataReader.Tag.Album = metadata.Album;
            _taglibMp3MetadataReader.Tag.Comment = metadata.Comments;
            _taglibMp3MetadataReader.Tag.Title = metadata.SongTitle;
            _taglibMp3MetadataReader.Tag.Track = (uint)metadata.TrackNumber;
            _taglibMp3MetadataReader.Tag.Year = (uint)metadata.Year;
        }

        #endregion
    }
}
